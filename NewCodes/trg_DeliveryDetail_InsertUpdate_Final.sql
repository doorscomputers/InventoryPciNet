USE [db_aa6a53_pcnet]
GO
/****** Object:  Trigger [dbo].[trg_DeliveryDetail_InsertUpdate]    Script Date: 11/13/2024 2:24:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER TRIGGER [dbo].[trg_DeliveryDetail_InsertUpdate]
ON [dbo].[DeliveryDetail]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Step 1: Calculate Previous Total Quantity and Total Cost
        DECLARE @TotalPreviousQuantity INT;
        DECLARE @TotalPreviousCost MONEY;

        SELECT 
            @TotalPreviousQuantity = SUM(Quantity),
            @TotalPreviousCost = SUM(Quantity * Cost)
        FROM dbo.BranchStock
        WHERE Stock = (SELECT DISTINCT Stock FROM inserted);

        -- Step 2: Calculate Current Total Cost for the Delivery
        DECLARE @QtyDelivered INT;
        DECLARE @CostPerUnit MONEY;
        DECLARE @CurrentTotalCost MONEY;

        SELECT TOP 1
            @QtyDelivered = QtyDelivered,
            @CostPerUnit = Cost
        FROM inserted;

        SET @CurrentTotalCost = @QtyDelivered * @CostPerUnit;

        -- Step 3: Calculate Total Inventory and Total Inventory Cost
        DECLARE @TotalInventory INT;
        DECLARE @TotalInventoryCost MONEY;

        SET @TotalInventory = @TotalPreviousQuantity + @QtyDelivered;
        SET @TotalInventoryCost = @TotalPreviousCost + @CurrentTotalCost;

        -- Step 4: Calculate Average Cost
        DECLARE @AverageCost MONEY;
        SET @AverageCost = CASE
                              WHEN @TotalInventory > 0 THEN @TotalInventoryCost / @TotalInventory
                              ELSE @CostPerUnit
                           END;

        -- Step 5: Update the Stock table with calculated values
        UPDATE S
        SET 
            S.LastDelivery = I.LastModifiedDate,
            S.LastSupplier = SU.[SupplierName],
            S.LastQtyDelivered = I.QtyDelivered,
            S.LastCost = S.Cost,
            S.LatestCost = @AverageCost,
            S.Cost = @AverageCost  -- Update the Stock.Cost with the new average cost
        FROM 
            [dbo].[Stock] S
        INNER JOIN 
            inserted I ON S.OID = I.Stock
        INNER JOIN 
            [dbo].[DeliveryHeader] DH ON I.DeliveryHeader = DH.OID
        INNER JOIN 
            [dbo].[Supplier] SU ON S.Supplier = SU.OID
        WHERE I.DeliveryHeader IS NOT NULL;

        -- Step 6: Update BranchStock table with the new average cost
        UPDATE BS
        SET BS.Cost = @AverageCost
        FROM dbo.BranchStock BS
        INNER JOIN inserted I ON BS.Stock = I.Stock
        WHERE I.DeliveryHeader IS NOT NULL;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
        END

        -- Log the error if necessary
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR (@ErrorMessage, 16, 1);
    END CATCH
END;
