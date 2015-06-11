IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Квиток]') AND type in (N'U'))
    DROP TABLE [dbo].[Квиток]
GO
CREATE TABLE [dbo].[Квиток] (
[№ замовлення] COUNTER  NULL  
, [Рейс] INTEGER  NULL  
, [№ місця] INTEGER  NULL  
, [id пасажира] INTEGER  NULL  
, [Метод оплати] VARCHAR(255)  NULL  
, [Сплачено] BIT  NULL  
, [Категорія замовлення] VARCHAR(255)  NULL  
)
GO

ALTER TABLE [dbo].[Квиток] ADD CONSTRAINT [Квиток_PK] PRIMARY KEY CLUSTERED (
[№ замовлення]
)
GO
CREATE   INDEX [Квитокid пасажира] ON [dbo].[Квиток] (
[id пасажира]
)
CREATE   INDEX [КвитокКатегорія замовлення] ON [dbo].[Квиток] (
[Категорія замовлення]
)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Тарифи]') AND type in (N'U'))
    DROP TABLE [dbo].[Тарифи]
GO
CREATE TABLE [dbo].[Тарифи] (
[Рейс] COUNTER  NULL  
, [Клас] INTEGER  NULL  
, [Тариф] CURRENCY  NULL  
)
GO

ALTER TABLE [dbo].[Тарифи] ADD CONSTRAINT [Тарифи_PK] PRIMARY KEY CLUSTERED (
[Рейс]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ціна]') AND type in (N'U'))
    DROP TABLE [dbo].[Ціна]
GO
CREATE TABLE [dbo].[Ціна] (
[Модель літака] VARCHAR(255)  NOT NULL  
, [Клас] VARCHAR(255)  NULL  
, [Ціна] INTEGER  NULL  
)
GO

ALTER TABLE [dbo].[Ціна] ADD CONSTRAINT [Ціна_PK] PRIMARY KEY CLUSTERED (
[Модель літака]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Клас]') AND type in (N'U'))
    DROP TABLE [dbo].[Клас]
GO
CREATE TABLE [dbo].[Клас] (
[Код класу] COUNTER  NULL  
, [Назва] VARCHAR(255)  NULL  
)
GO

ALTER TABLE [dbo].[Клас] ADD CONSTRAINT [Клас_PK] PRIMARY KEY CLUSTERED (
[Код класу]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Місця]') AND type in (N'U'))
    DROP TABLE [dbo].[Місця]
GO
CREATE TABLE [dbo].[Місця] (
[Рейс] COUNTER  NULL  
, [№ місця] INTEGER  NULL  
, [Код класу] INTEGER  NULL  
, [Вільне місце] INTEGER  NULL  
)
GO

ALTER TABLE [dbo].[Місця] ADD CONSTRAINT [Місця_PK] PRIMARY KEY CLUSTERED (
[Рейс]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Пасажир]') AND type in (N'U'))
    DROP TABLE [dbo].[Пасажир]
GO
CREATE TABLE [dbo].[Пасажир] (
[id пасажира] COUNTER  NULL  
, [Прізвище] VARCHAR(255)  NULL  
, [Ім’я] VARCHAR(255)  NULL  
, [По батькові] VARCHAR(255)  NULL  
, [Стать] VARCHAR(255)  NULL  
, [Дата народження] DATETIME  NULL  
, [Документ] VARCHAR(255)  NULL  
)
GO

ALTER TABLE [dbo].[Пасажир] ADD CONSTRAINT [Пасажир_PK] PRIMARY KEY CLUSTERED (
[id пасажира]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Рейс]') AND type in (N'U'))
    DROP TABLE [dbo].[Рейс]
GO
CREATE TABLE [dbo].[Рейс] (
[Код рейсу] INTEGER  NOT NULL  
, [Модель літака] VARCHAR(255)  NULL  
, [Час відправлення] DATETIME  NULL  
, [Час прибуття] DATETIME  NULL  
, [Пункт відправлення] LONGCHAR  NULL  
, [Пункт прибуття] LONGCHAR  NULL  
, [Код аеропорту] INTEGER  NULL  
)
GO

ALTER TABLE [dbo].[Рейс] ADD CONSTRAINT [Рейс_PK] PRIMARY KEY CLUSTERED (
[Код рейсу]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Аеропорт]') AND type in (N'U'))
    DROP TABLE [dbo].[Аеропорт]
GO
CREATE TABLE [dbo].[Аеропорт] (
[Код аеропорта] INTEGER  NOT NULL  
, [Назва] LONGCHAR  NULL  
, [Рейс] VARCHAR(255)  NULL  
)
GO

ALTER TABLE [dbo].[Аеропорт] ADD CONSTRAINT [Аеропорт_PK] PRIMARY KEY CLUSTERED (
[Код аеропорта]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Категорія замовлень]') AND type in (N'U'))
    DROP TABLE [dbo].[Категорія замовлень]
GO
CREATE TABLE [dbo].[Категорія замовлень] (
[Категорія замовлення] COUNTER  NULL  
, [Назва] VARCHAR(255)  NULL  
)
GO

ALTER TABLE [dbo].[Категорія замовлень] ADD CONSTRAINT [Категорія замовлень_PK] PRIMARY KEY CLUSTERED (
[Категорія замовлення]
)
GO
GO

ALTER TABLE [dbo].[Квиток] WITH CHECK ADD CONSTRAINT [РейсКвиток] FOREIGN KEY (
[Рейс]
)
REFERENCES [dbo].[Рейс] (
[Код рейсу]
)
ALTER TABLE [dbo].[Квиток] WITH CHECK ADD CONSTRAINT [ПасажирКвиток] FOREIGN KEY (
[id пасажира]
)
REFERENCES [dbo].[Пасажир] (
[id пасажира]
)
ALTER TABLE [dbo].[Квиток] WITH CHECK ADD CONSTRAINT [Категорія замовленьКвиток] FOREIGN KEY (
[Категорія замовлення]
)
REFERENCES [dbo].[Категорія замовлень] (
[Категорія замовлення]
)
GO

ALTER TABLE [dbo].[Тарифи] WITH CHECK ADD CONSTRAINT [МісцяТарифи] FOREIGN KEY (
[Рейс]
)
REFERENCES [dbo].[Місця] (
[Рейс]
)
ALTER TABLE [dbo].[Тарифи] WITH CHECK ADD CONSTRAINT [КласТарифи] FOREIGN KEY (
[Клас]
)
REFERENCES [dbo].[Клас] (
[Код класу]
)
GO

GO

GO

GO

GO

ALTER TABLE [dbo].[Рейс] WITH CHECK ADD CONSTRAINT [ЦінаРейс] FOREIGN KEY (
[Модель літака]
)
REFERENCES [dbo].[Ціна] (
[Модель літака]
)
ALTER TABLE [dbo].[Рейс] WITH CHECK ADD CONSTRAINT [ТарифиРейс] FOREIGN KEY (
[Код рейсу]
)
REFERENCES [dbo].[Тарифи] (
[Рейс]
)
ALTER TABLE [dbo].[Рейс] WITH CHECK ADD CONSTRAINT [МісцяРейс] FOREIGN KEY (
[Код рейсу]
)
REFERENCES [dbo].[Місця] (
[Рейс]
)
ALTER TABLE [dbo].[Рейс] WITH CHECK ADD CONSTRAINT [АеропортРейс] FOREIGN KEY (
[Код аеропорту]
)
REFERENCES [dbo].[Аеропорт] (
[Код аеропорта]
)
GO

GO

GO

