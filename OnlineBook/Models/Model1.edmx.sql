
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/06/2024 14:26:18
-- Generated from EDMX file: C:\Users\PC\Downloads\Book v5\Book v5\OnlineBook\OnlineBook\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EBookpvtDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Book__categoryId__3A81B327]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Book] DROP CONSTRAINT [FK__Book__categoryId__3A81B327];
GO
IF OBJECT_ID(N'[dbo].[FK__Feedback__bookId__17036CC0]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedback] DROP CONSTRAINT [FK__Feedback__bookId__17036CC0];
GO
IF OBJECT_ID(N'[dbo].[FK__Feedback__userId__160F4887]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedback] DROP CONSTRAINT [FK__Feedback__userId__160F4887];
GO
IF OBJECT_ID(N'[dbo].[FK__Orders__bookId__73BA3083]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK__Orders__bookId__73BA3083];
GO
IF OBJECT_ID(N'[dbo].[FK__Orders__invoiceI__74AE54BC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK__Orders__invoiceI__74AE54BC];
GO
IF OBJECT_ID(N'[dbo].[FK__Orders__userId__72C60C4A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK__Orders__userId__72C60C4A];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin];
GO
IF OBJECT_ID(N'[dbo].[Book]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Book];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Feedback]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feedback];
GO
IF OBJECT_ID(N'[dbo].[Invoice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Invoice];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [bookId] int IDENTITY(1,1) NOT NULL,
    [bookName] varchar(255)  NOT NULL,
    [bookAuthor] varchar(255)  NOT NULL,
    [bookDescription] varchar(max)  NOT NULL,
    [bookPrice] float  NOT NULL,
    [bookQuantity] int  NOT NULL,
    [bookImage] varchar(max)  NOT NULL,
    [categoryId] int  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [userId] int IDENTITY(1,1) NOT NULL,
    [userFirstName] varchar(255)  NOT NULL,
    [userLastName] varchar(255)  NOT NULL,
    [userNIC] varchar(15)  NOT NULL,
    [userDOB] datetime  NOT NULL,
    [userPhone] int  NOT NULL,
    [userEmail] varchar(100)  NOT NULL,
    [userpassword] varchar(100)  NOT NULL,
    [userAddress] varchar(255)  NOT NULL,
    [userRole] char(1)  NOT NULL
);
GO

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [adminId] int IDENTITY(1,1) NOT NULL,
    [adminFirstName] varchar(255)  NOT NULL,
    [adminLastName] varchar(255)  NOT NULL,
    [adminEmail] varchar(100)  NOT NULL,
    [adminpassword] varchar(100)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [categoryId] int IDENTITY(1,1) NOT NULL,
    [categoryName] varchar(255)  NOT NULL,
    [categoryStatus] int  NULL,
    [categoryImage] varchar(max)  NULL
);
GO

-- Creating table 'Feedbacks'
CREATE TABLE [dbo].[Feedbacks] (
    [feddbackId] int IDENTITY(1,1) NOT NULL,
    [feedbackDescription] varchar(max)  NOT NULL,
    [feedbackDate] datetime  NOT NULL,
    [userId] int  NULL,
    [bookId] int  NULL,
    [userName] varchar(max)  NULL
);
GO

-- Creating table 'Invoices'
CREATE TABLE [dbo].[Invoices] (
    [invoiceId] int IDENTITY(1,1) NOT NULL,
    [invoiceDate] datetime  NOT NULL,
    [userId] int  NULL,
    [invoicetotal] float  NULL,
    [userName] varchar(max)  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [orderId] int IDENTITY(1,1) NOT NULL,
    [orderDate] datetime  NOT NULL,
    [userId] int  NULL,
    [bookId] int  NULL,
    [invoiceId] int  NULL,
    [orderQty] int  NULL,
    [orderBill] float  NULL,
    [orderUnitPrice] float  NULL,
    [bookName] varchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [bookId] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([bookId] ASC);
GO

-- Creating primary key on [userId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- Creating primary key on [adminId] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([adminId] ASC);
GO

-- Creating primary key on [categoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([categoryId] ASC);
GO

-- Creating primary key on [feddbackId] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [PK_Feedbacks]
    PRIMARY KEY CLUSTERED ([feddbackId] ASC);
GO

-- Creating primary key on [invoiceId] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [PK_Invoices]
    PRIMARY KEY CLUSTERED ([invoiceId] ASC);
GO

-- Creating primary key on [orderId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([orderId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [categoryId] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [FK__Book__categoryId__3A81B327]
    FOREIGN KEY ([categoryId])
    REFERENCES [dbo].[Categories]
        ([categoryId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Book__categoryId__3A81B327'
CREATE INDEX [IX_FK__Book__categoryId__3A81B327]
ON [dbo].[Books]
    ([categoryId]);
GO

-- Creating foreign key on [bookId] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK__Feedback__bookId__17036CC0]
    FOREIGN KEY ([bookId])
    REFERENCES [dbo].[Books]
        ([bookId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Feedback__bookId__17036CC0'
CREATE INDEX [IX_FK__Feedback__bookId__17036CC0]
ON [dbo].[Feedbacks]
    ([bookId]);
GO

-- Creating foreign key on [userId] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK__Feedback__userId__160F4887]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[Users]
        ([userId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Feedback__userId__160F4887'
CREATE INDEX [IX_FK__Feedback__userId__160F4887]
ON [dbo].[Feedbacks]
    ([userId]);
GO

-- Creating foreign key on [bookId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK__Orders__bookId__73BA3083]
    FOREIGN KEY ([bookId])
    REFERENCES [dbo].[Books]
        ([bookId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Orders__bookId__73BA3083'
CREATE INDEX [IX_FK__Orders__bookId__73BA3083]
ON [dbo].[Orders]
    ([bookId]);
GO

-- Creating foreign key on [invoiceId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK__Orders__invoiceI__74AE54BC]
    FOREIGN KEY ([invoiceId])
    REFERENCES [dbo].[Invoices]
        ([invoiceId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Orders__invoiceI__74AE54BC'
CREATE INDEX [IX_FK__Orders__invoiceI__74AE54BC]
ON [dbo].[Orders]
    ([invoiceId]);
GO

-- Creating foreign key on [userId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK__Orders__userId__72C60C4A]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[Users]
        ([userId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Orders__userId__72C60C4A'
CREATE INDEX [IX_FK__Orders__userId__72C60C4A]
ON [dbo].[Orders]
    ([userId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------