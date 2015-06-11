IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Аеропорт]') AND type in (N'U'))
    DROP TABLE [dbo].[Аеропорт]
GO
CREATE TABLE [dbo].[Аеропорт] (
[Назва] CHAR(10)  NOT NULL  
, [Код аеропорту] INTEGER  NOT NULL  
, [Адрес] CHAR(10)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Аеропорт] ADD CONSTRAINT [Аеропорт_PK] PRIMARY KEY CLUSTERED (
[Код аеропорту]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Рейс]') AND type in (N'U'))
    DROP TABLE [dbo].[Рейс]
GO
CREATE TABLE [dbo].[Рейс] (
[Рейс] INTEGER  NOT NULL  
, [Пункт відправки] CHAR(10)  NOT NULL  
, [Пункт призначення] CHAR(10)  NOT NULL  
, [Час відправки] DATETIME  NOT NULL  
, [Час призначення] DATETIME  NOT NULL  
, [Код аеропорту] INTEGER  NOT NULL  
)
GO

ALTER TABLE [dbo].[Рейс] ADD CONSTRAINT [Рейс_PK] PRIMARY KEY CLUSTERED (
[Рейс]
)
GO
CREATE   INDEX [Рейс] ON [dbo].[Рейс] (
[Час відправки]
, [Час призначення]
)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Місця]') AND type in (N'U'))
    DROP TABLE [dbo].[Місця]
GO
CREATE TABLE [dbo].[Місця] (
[Рейс] INTEGER  NOT NULL  
, [№ місця] COUNTER  NOT NULL  
, [Клас] INTEGER  NOT NULL  
, [Вільне місце] COUNTER  NOT NULL  
)
GO

ALTER TABLE [dbo].[Місця] ADD CONSTRAINT [Місця_PK] PRIMARY KEY CLUSTERED (
[Рейс]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Квиток]') AND type in (N'U'))
    DROP TABLE [dbo].[Квиток]
GO
CREATE TABLE [dbo].[Квиток] (
[№ замовлення] CHAR(10)  NOT NULL  
, [Рейс] INTEGER  NOT NULL  
, [№ місця] COUNTER  NOT NULL  
, [id пасажира] INTEGER  NOT NULL  
, [Метод оплати] CHAR(10)  NOT NULL  
, [Сплачено] BINARY(10)  NOT NULL  
, [Категорія замовлення] INTEGER  NULL  
)
GO

ALTER TABLE [dbo].[Квиток] ADD CONSTRAINT [Квиток_PK] PRIMARY KEY CLUSTERED (
[№ замовлення]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Пасажир]') AND type in (N'U'))
    DROP TABLE [dbo].[Пасажир]
GO
CREATE TABLE [dbo].[Пасажир] (
[id пасажира] INTEGER  NOT NULL  
, [Прізвище] CHAR(10)  NOT NULL  
, [Ім’я] CHAR(10)  NOT NULL  
, [По батькові] CHAR(10)  NOT NULL  
, [Стать] CHAR(10)  NULL  
, [День народження] DATETIME  NULL  
, [Документ] CHAR(10)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Пасажир] ADD CONSTRAINT [Пасажир_PK] PRIMARY KEY CLUSTERED (
[id пасажира]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Категорія замовлення]') AND type in (N'U'))
    DROP TABLE [dbo].[Категорія замовлення]
GO
CREATE TABLE [dbo].[Категорія замовлення] (
[Категорія замовлення] INTEGER  NOT NULL  
, [Назва] CHAR(10)  NOT NULL  
)
GO

ALTER TABLE [dbo].[Категорія замовлення] ADD CONSTRAINT [Категорія замовлення_PK] PRIMARY KEY CLUSTERED (
[Категорія замовлення]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Клас]') AND type in (N'U'))
    DROP TABLE [dbo].[Клас]
GO
CREATE TABLE [dbo].[Клас] (
[Код класу] INTEGER  NOT NULL  
, [Назва] CHAR(10)  NOT NULL  
, [Рейс] INTEGER  NULL  
)
GO

ALTER TABLE [dbo].[Клас] ADD CONSTRAINT [Клас_PK] PRIMARY KEY CLUSTERED (
[Код класу]
)
GO
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Тарифи]') AND type in (N'U'))
    DROP TABLE [dbo].[Тарифи]
GO
CREATE TABLE [dbo].[Тарифи] (
[Код класу] INTEGER  NOT NULL  
, [Тариф] DOUBLE  NOT NULL  
, [Рейс] INTEGER  NOT NULL  
)
GO

ALTER TABLE [dbo].[Тарифи] ADD CONSTRAINT [Тарифи_PK] PRIMARY KEY CLUSTERED (
[Рейс]
)
GO
GO

ALTER TABLE [dbo].[Аеропорт] WITH CHECK ADD CONSTRAINT [Рейс_Аеропорт_FK1] FOREIGN KEY (
[Код аеропорту]
)
REFERENCES [dbo].[Рейс] (
[Рейс]
)
GO

GO

ALTER TABLE [dbo].[Місця] WITH CHECK ADD CONSTRAINT [Тарифи_Місця_FK1] FOREIGN KEY (
[Рейс]
)
REFERENCES [dbo].[Тарифи] (
[Рейс]
)
GO

ALTER TABLE [dbo].[Квиток] WITH CHECK ADD CONSTRAINT [Рейс_Квиток_FK1] FOREIGN KEY (
[Рейс]
)
REFERENCES [dbo].[Рейс] (
[Рейс]
)
GO

ALTER TABLE [dbo].[Пасажир] WITH CHECK ADD CONSTRAINT [Квиток_Пасажир_FK1] FOREIGN KEY (
[id пасажира]
)
REFERENCES [dbo].[Квиток] (
[id пасажира]
)
GO

ALTER TABLE [dbo].[Категорія замовлення] WITH CHECK ADD CONSTRAINT [Квиток_Категорія замовлення_FK1] FOREIGN KEY (
[Категорія замовлення]
)
REFERENCES [dbo].[Квиток] (
[Категорія замовлення]
)
GO

ALTER TABLE [dbo].[Клас] WITH CHECK ADD CONSTRAINT [Тарифи_Клас_FK2] FOREIGN KEY (
[Рейс]
, [Код класу]
)
REFERENCES [dbo].[Тарифи] (
[Рейс]
, [Код класу]
)
ALTER TABLE [dbo].[Клас] WITH CHECK ADD CONSTRAINT [Тарифи_Клас_FK1] FOREIGN KEY (
[Рейс]
, [Код класу]
)
REFERENCES [dbo].[Тарифи] (
[Рейс]
, [Код класу]
)
ALTER TABLE [dbo].[Клас] WITH CHECK ADD CONSTRAINT [Тарифи_Клас_FK3] FOREIGN KEY (
[Рейс]
, [Код класу]
)
REFERENCES [dbo].[Тарифи] (
[Рейс]
, [Код класу]
)
GO

ALTER TABLE [dbo].[Тарифи] WITH CHECK ADD CONSTRAINT [Рейс_Тарифи_FK1] FOREIGN KEY (
)
REFERENCES [dbo].[Рейс] (
)
GO

