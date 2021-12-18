IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200624220136_Initial', N'3.1.0');

GO

CREATE TABLE [Store] (
    [StoreId] int NOT NULL IDENTITY,
    [Title] nvarchar(200) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Administrator] int NOT NULL,
    [IsDeleted] bit NULL,
    [CreatedBy] nvarchar(450) NULL,
    [Created] datetime NULL DEFAULT ((getdate())),
    [LastModifiedBy] nvarchar(450) NULL,
    [LastModified] datetime NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY ([StoreId]),
    CONSTRAINT [FK_Store_Employee] FOREIGN KEY ([Administrator]) REFERENCES [Employee] ([OID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ValueGroup] (
    [ValueGroupId] int NOT NULL IDENTITY,
    [Value] nvarchar(450) NULL,
    [Group] nvarchar(450) NULL,
    [IsDeleted] bit NULL,
    [CreatedBy] nvarchar(450) NULL,
    [Created] datetime NULL DEFAULT ((getdate())),
    [LastModifiedBy] nvarchar(450) NULL,
    [LastModified] datetime2 NULL,
    CONSTRAINT [PK_ValueGroup] PRIMARY KEY ([ValueGroupId])
);

GO

CREATE TABLE [SparePart] (
    [SparePartId] int NOT NULL IDENTITY,
    [Barcode] nvarchar(200) NULL,
    [Title] nvarchar(max) NULL,
    [Category] int NULL,
    [Description] nvarchar(max) NULL,
    [IsDeleted] bit NULL,
    [CreatedBy] nvarchar(450) NULL,
    [Created] datetime NULL DEFAULT ((getdate())),
    [LastModifiedBy] nvarchar(450) NULL,
    [LastModified] datetime NULL,
    CONSTRAINT [PK_SparePart] PRIMARY KEY ([SparePartId]),
    CONSTRAINT [FK_SparePart_ValueObject] FOREIGN KEY ([Category]) REFERENCES [ValueGroup] ([ValueGroupId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Inventory] (
    [InventoryId] int NOT NULL IDENTITY,
    [StoreId] int NOT NULL,
    [SparePartId] int NOT NULL,
    [OpeningBalance] int NOT NULL,
    [CurrentBalance] int NOT NULL,
    [IsDeleted] bit NULL,
    [CreatedBy] nvarchar(450) NULL,
    [Created] datetime NULL DEFAULT ((getdate())),
    [LastModifiedBy] nvarchar(450) NULL,
    [LastModified] datetime2 NULL,
    CONSTRAINT [PK_Inventory] PRIMARY KEY ([InventoryId]),
    CONSTRAINT [FK_Inventory_SparePart] FOREIGN KEY ([SparePartId]) REFERENCES [SparePart] ([SparePartId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Inventory_Store] FOREIGN KEY ([StoreId]) REFERENCES [Store] ([StoreId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [InventoryTransaction] (
    [TransactionId] int NOT NULL IDENTITY,
    [InventoryId] int NOT NULL,
    [Type] int NULL,
    [Amount] int NULL,
    [Description] nvarchar(max) NULL,
    [ReferenceType] int NULL,
    [ReferenceId] nvarchar(max) NULL,
    [IsDeleted] bit NULL,
    [CreatedBy] nvarchar(450) NULL,
    [Created] datetime NULL DEFAULT ((getdate())),
    [LastModifiedBy] nvarchar(450) NULL,
    [LastModified] datetime2 NULL,
    CONSTRAINT [PK_InventoryTransaction] PRIMARY KEY ([TransactionId]),
    CONSTRAINT [FK_InventoryTransaction_Inventory] FOREIGN KEY ([InventoryId]) REFERENCES [Inventory] ([InventoryId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_InventoryTransaction_ValueObject] FOREIGN KEY ([Type]) REFERENCES [ValueGroup] ([ValueGroupId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Inventory_SparePartId] ON [Inventory] ([SparePartId]);

GO

CREATE INDEX [IX_Inventory_StoreId] ON [Inventory] ([StoreId]);

GO

CREATE INDEX [IX_InventoryTransaction_InventoryId] ON [InventoryTransaction] ([InventoryId]);

GO

CREATE INDEX [IX_InventoryTransaction_Type] ON [InventoryTransaction] ([Type]);

GO

CREATE INDEX [IX_SparePart_Category] ON [SparePart] ([Category]);

GO

CREATE INDEX [IX_Store_Administrator] ON [Store] ([Administrator]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200627044100_StoresEntities', N'3.1.0');

GO

CREATE TABLE [SMSMessage] (
    [Id] int NOT NULL IDENTITY,
    [Text] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    [CreatedBy] nvarchar(max) NULL,
    [Created] datetime2 NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [LastModified] datetime2 NULL,
    CONSTRAINT [PK_SMSMessage] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [OrderSMSMessage] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [SMSMessageId] int NOT NULL,
    [Sent] bit NOT NULL,
    [Report] nvarchar(max) NULL,
    [CreatedBy] nvarchar(max) NULL,
    [Created] datetime2 NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    [LastModified] datetime2 NULL,
    CONSTRAINT [PK_OrderSMSMessage] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderSMSMessage_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([OID]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderSMSMessage_SMSMessage_SMSMessageId] FOREIGN KEY ([SMSMessageId]) REFERENCES [SMSMessage] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_OrderSMSMessage_OrderId] ON [OrderSMSMessage] ([OrderId]);

GO

CREATE INDEX [IX_OrderSMSMessage_SMSMessageId] ON [OrderSMSMessage] ([SMSMessageId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200717071857_SMSInitial', N'3.1.0');

GO

