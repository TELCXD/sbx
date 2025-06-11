CREATE DATABASE sbx_development
GO
USE sbx_development
GO
CREATE TABLE T_Profile
(
IdProfile INT IDENTITY(1,1) PRIMARY KEY,
NameProfile VARCHAR(30) UNIQUE,
Administrador BIT,
Active BIT
)
GO
INSERT INTO T_Profile (NameProfile,Administrador,Active)
VALUES('Administrador',1,1),('Cajero',0,1)
GO
CREATE TABLE T_Role
(
IdRole INT IDENTITY(1,1) PRIMARY KEY,
NameRole VARCHAR(30) UNIQUE,
IdProfile INT,
Active BIT,
IdUserAction INT,
CreationDate DATETIME,
UpdateDate DATETIME,
FOREIGN KEY(IdProfile) REFERENCES T_Profile(IdProfile)
)
GO
INSERT INTO T_Role (NameRole,IdProfile,Active,CreationDate)
VALUES ('Administrador',1,1,GETDATE()), ('Cajero',2,1,GETDATE())
GO
CREATE TABLE T_IdentificationType
(
IdIdentificationType INT IDENTITY(1,1) PRIMARY KEY,
IdentificationType VARCHAR(50) UNIQUE
)
GO
INSERT INTO T_IdentificationType (IdentificationType)
VALUES ('CC-Cédula de ciudadanía'), ('CE-Cédula de extranjería'), ('RUT'), ('NIT')
GO
CREATE TABLE T_Country(
IdCountry INT IDENTITY(1,1) PRIMARY KEY,
Code INT NOT NULL UNIQUE,
CountryName VARCHAR(100) NOT NULL UNIQUE
)
GO
INSERT INTO T_Country (Code, CountryName) VALUES(169,'COLOMBIA')
GO
CREATE TABLE T_Departament(
IdDepartament INT IDENTITY(1,1) PRIMARY KEY,
Code INT NOT NULL UNIQUE,
DepartmentName VARCHAR(100) NOT NULL UNIQUE,
IdCountry INT NOT NULL,
FOREIGN KEY(IdCountry) REFERENCES T_Country(IdCountry)
)
GO
INSERT INTO T_Departament (Code, DepartmentName,IdCountry) 
VALUES
	(76,'VALLE DEL CAUCA',1),
	(41,'HUILA',1)
GO
CREATE TABLE T_City(
IdCity INT IDENTITY(1,1) PRIMARY KEY,
Code INT NOT NULL UNIQUE,
CityName VARCHAR(100) NOT NULL UNIQUE,
IdDepartment INT NOT NULL,
FOREIGN KEY(IdDepartment) REFERENCES T_Departament(IdDepartament)
)
GO
INSERT INTO T_City (Code, CityName,IdDepartment) 
VALUES
	(76001,'CALI',1),
	(41551,'PITALITO',1),
	(76130,'CANDELARIA',1)
GO
CREATE TABLE T_User
(
IdUser INT IDENTITY(1,1) PRIMARY KEY,
IdIdentificationType INT,
IdentificationNumber VARCHAR(20) UNIQUE,
Name VARCHAR(50),
LastName VARCHAR(50),
BirthDate DATE,
IdCountry INT,
IdDepartament INT,
IdCity INT,
TelephoneNumber VARCHAR(20) UNIQUE,
Email VARCHAR(50) UNIQUE,
IdRole INT,
UserName VARCHAR(50) UNIQUE,
Password VARCHAR(MAX),
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
Active BIT,
FOREIGN KEY(IdIdentificationType) REFERENCES T_IdentificationType(IdIdentificationType),
FOREIGN KEY(IdCountry) REFERENCES T_Country(IdCountry),
FOREIGN KEY(IdDepartament) REFERENCES T_Departament(IdDepartament),
FOREIGN KEY(IdCity) REFERENCES T_City(IdCity),
FOREIGN KEY(IdRole) REFERENCES T_Role(IdRole),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
INSERT INTO T_User (IdIdentificationType,IdentificationNumber,Name,LastName,BirthDate,IdCountry,IdDepartament,IdCity,TelephoneNumber,Email,IdRole,UserName,Password,CreationDate,Active)
VALUES
(1,'1113528027','Ruben','Cordoba','1993-05-07',1,1,1,'3137450103','ruben0793@hotmail.com',1,'Ruben','$argon2id$v=19$m=65536,t=3,p=1$r1zN0nle0xnH8cGWgGWMqw$R37Ws1t8521Tj9AXjFY593sBQsmsuVZ6KFScE1ueFeY',GETDATE(),1),
(1,'111','Admin','Admin','1993-05-07',1,1,1,'0','',1,'admin','$argon2id$v=19$m=65536,t=3,p=1$dcutl4S74zG2Qnhjj6vdUQ$jljJ67rhubEv1oWa02Dio9pj97sz8qen+ZUGcH/QiyM',GETDATE(),1)
GO
CREATE TABLE T_Menu
(
IdMenu INT IDENTITY(1,1) PRIMARY KEY,
MenuName VARCHAR(50) UNIQUE,
DescripcionMenu VARCHAR(200),
IdMenuParent INT,
OrderNumber INT,
MenuURL VARCHAR(200),
IdIcon INT,
Active BIT,
IdUserAction INT,
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
INSERT INTO T_Menu (MenuName,OrderNumber,MenuURL,Active,IdUserAction)
VALUES ('Home',1,'home',1,1), ('Ventas',2,'ventas',1,1), ('Productos',3,'productos',1,1), ('Inventario',4,'inventario',1,1), 
('Clientes',5,'clientes',1,1), ('Proveedor',6,'proveedor',1,1),('Caja',7,'caja',1,1), ('NotaCredito',8,'notaCredito',1,1), 
('Domicilios',9,'domicilios',1,1), ('Reportes',10,'reportes',1,1), ('Ajustes',11,'ajustes',1,1), ('Tienda',12,'tienda',1,1), 
('Entrada',13,'entradas',1,1), ('Salida',14,'salidas',1,1),('PreciosClientes',15,'preciosClientes',1,1),
('ListaPrecios',16,'listaPrecios',1,1), ('Promociones',17,'promociones',1,1),('Usuarios',18,'usuarios',1,1)
,('Permisos',19,'permisos',1,1)
GO
CREATE TABLE TR_User_Menu
(
IdUserMenu INT IDENTITY(1,1) PRIMARY KEY,
IdMenu INT,
IdUser INT,
ToRead BIT,
ToCreate BIT,
ToUpdate BIT,
ToDelete BIT,
Active BIT,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY(IdMenu) REFERENCES T_Menu(IdMenu),
FOREIGN KEY(IdUser) REFERENCES T_User(IdUser),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
INSERT INTO TR_User_Menu (IdMenu, IdUser, ToRead, ToCreate, ToUpdate, ToDelete, Active,CreationDate, IdUserAction)
VALUES(1,1,1,1,1,1,1,GETDATE(),1), (2,1,1,1,1,1,1,GETDATE(),1), (3,1,1,1,1,1,1,GETDATE(),1), (4,1,1,1,1,1,1,GETDATE(),1), 
(5,1,1,1,1,1,1,GETDATE(),1), (6,1,1,1,1,1,1,GETDATE(),1), (7,1,1,1,1,1,1,GETDATE(),1),
(8,1,1,1,1,1,1,GETDATE(),1), (9,1,1,1,1,1,1,GETDATE(),1), (10,1,1,1,1,1,1,GETDATE(),1), (11,1,1,1,1,1,1,GETDATE(),1), 
(12,1,1,1,1,1,1,GETDATE(),1), (13,1,1,1,1,1,1,GETDATE(),1), (14,1,1,1,1,1,1,GETDATE(),1), (15,1,1,1,1,1,1,GETDATE(),1),
(16,1,1,1,1,1,1,GETDATE(),1), (17,1,1,1,1,1,1,GETDATE(),1), (18,1,1,1,1,1,1,GETDATE(),1), (19,1,1,1,1,1,1,GETDATE(),1),

(1,2,1,1,1,1,1,GETDATE(),1), (2,2,1,1,1,1,1,GETDATE(),1), (3,2,1,1,1,1,1,GETDATE(),1), (4,2,1,1,1,1,1,GETDATE(),1), 
(5,2,1,1,1,1,1,GETDATE(),1), (6,2,1,1,1,1,1,GETDATE(),1), (7,2,1,1,1,1,1,GETDATE(),1),
(8,2,1,1,1,1,1,GETDATE(),1), (9,2,1,1,1,1,1,GETDATE(),1), (10,2,1,1,1,1,1,GETDATE(),1), (11,2,1,1,1,1,1,GETDATE(),1), 
(12,2,1,1,1,1,1,GETDATE(),1), (13,2,1,1,1,1,1,GETDATE(),1), (14,2,1,1,1,1,1,GETDATE(),1), (15,2,1,1,1,1,1,GETDATE(),1),
(16,2,1,1,1,1,1,GETDATE(),1), (17,2,1,1,1,1,1,GETDATE(),1), (18,2,1,1,1,1,1,GETDATE(),1), (19,2,1,1,1,1,1,GETDATE(),1)
GO
CREATE TABLE T_TipoResponsabilidad(
IdTipoResponsabilidad INT PRIMARY KEY,
Code VARCHAR(20) UNIQUE,
Nombre VARCHAR(100) UNIQUE
)
GO
INSERT INTO T_TipoResponsabilidad (IdTipoResponsabilidad, Code, Nombre)
VALUES(1,'R-99-PN','NO RESPONSABLE'),(2,'O-15','AUTORRETENEDOR'),(3,'O-23','AGENTE DE RETENCIÓN IVA'),(4,'O-47','RÉGIMEN SIMPLE DE TRIBUTACIÓN'),
(5,'O-13','GRAN CONTRIBUYENTE')
GO
CREATE TABLE T_ResponsabilidadTributaria(
IdResponsabilidadTributaria INT PRIMARY KEY,
Code VARCHAR(20) UNIQUE,
Nombre VARCHAR(100) UNIQUE
)
GO
INSERT INTO T_ResponsabilidadTributaria (IdResponsabilidadTributaria,Code,Nombre)
VALUES(1,'ZZ','No aplica'),(2,'01','IVA'),(3,'04','INC'),(4,'ZA','IVA e INC')
GO
CREATE TABLE T_TipoContribuyente(
IdTipoContribuyente INT PRIMARY KEY,
Code VARCHAR(20),
Nombre VARCHAR(100) UNIQUE
)
GO
INSERT INTO T_TipoContribuyente (IdTipoContribuyente,Nombre)
VALUES(1,'Persona Natural y asimiladas'),(2,'Persona Jurídica y asimiladas')
GO
CREATE TABLE T_CodigoPostal(
IdCodigoPostal INT PRIMARY KEY,
Code VARCHAR(20) UNIQUE
)
GO
INSERT INTO T_CodigoPostal (IdCodigoPostal,Code)
VALUES(1,'N/A')
GO
CREATE TABLE T_ActividadEconomica(
IdActividadEconomica INT PRIMARY KEY,
Code VARCHAR(20) UNIQUE,
Nombre VARCHAR(100) UNIQUE
)
GO
INSERT INTO T_ActividadEconomica (IdActividadEconomica,Nombre)
VALUES(1,'N/A')
GO
CREATE TABLE T_Tienda(
IdTienda INT PRIMARY KEY,
IdIdentificationType INT,
NumeroDocumento VARCHAR(11) UNIQUE,
NombreRazonSocial VARCHAR(100) UNIQUE,
IdTipoResponsabilidad INT,
IdResponsabilidadTributaria INT,
IdTipoContribuyente INT,
CorreoDistribucion VARCHAR(100) UNIQUE,
Telefono VARCHAR(10) UNIQUE,
Direccion VARCHAR(200),
IdCountry INT,
IdDepartament INT,
IdCity INT,
IdCodigoPostal INT,
IdActividadEconomica INT,
Logo IMAGE,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT
FOREIGN KEY(IdIdentificationType) REFERENCES T_IdentificationType(IdIdentificationType),
FOREIGN KEY(IdResponsabilidadTributaria) REFERENCES T_ResponsabilidadTributaria(IdResponsabilidadTributaria),
FOREIGN KEY(IdTipoContribuyente) REFERENCES T_TipoContribuyente(IdTipoContribuyente),
FOREIGN KEY(IdCountry) REFERENCES T_Country(IdCountry),
FOREIGN KEY(IdDepartament) REFERENCES T_Departament(IdDepartament),
FOREIGN KEY(IdCity) REFERENCES T_City(IdCity),
FOREIGN KEY(IdCodigoPostal) REFERENCES T_CodigoPostal(IdCodigoPostal),
FOREIGN KEY(IdActividadEconomica) REFERENCES T_ActividadEconomica(IdActividadEconomica),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_TipoDocumentoRangoNumeracion(
Id_TipoDocumentoRangoNumeracion INT PRIMARY KEY,
Nombre VARCHAR(100) UNIQUE
)
GO
INSERT INTO T_TipoDocumentoRangoNumeracion(Id_TipoDocumentoRangoNumeracion,Nombre)
VALUES(1,'Factura'),(2,'Factura Electrónica de Venta'),(3,'Nota de Crédito'),(4,'Nota de debito')
GO
CREATE TABLE T_RangoNumeracion(
Id_RangoNumeracion INT IDENTITY(1,1) PRIMARY KEY,
Id_TipoDocumentoRangoNumeracion INT,
Prefijo VARCHAR(10) UNIQUE,
NumeroDesde BIGINT,
NumeroHasta BIGINT,
NumeroAutorizacion VARCHAR(50) UNIQUE,
FechaVencimiento DATE,
Active BIT,
NumeracionAutorizada BIT,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY(Id_TipoDocumentoRangoNumeracion) REFERENCES T_TipoDocumentoRangoNumeracion(Id_TipoDocumentoRangoNumeracion),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
INSERT INTO T_RangoNumeracion (Id_TipoDocumentoRangoNumeracion, Prefijo, NumeroDesde,NumeroHasta,FechaVencimiento,Active,CreationDate,IdUserAction)
VALUES(1,'FV',1,9999999,'2030-01-01',1,GETDATE(),1)
GO
CREATE TABLE T_Categorias(
IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(100) UNIQUE
)
GO
INSERT INTO T_Categorias(Nombre) VALUES('N/A'),('Papelería y Oficina'),('Alimentos y Bebidas'),('Limpieza y Hogar'),('Bebés y Maternidad'),('Tecnología y Electrónica'),
									   ('Ropa y Calzado'),('Ferretería y Herramientas'),('Mascotas y Accesorios'),('Medicamentos y Farmacia'),('Juguetes y Juegos'),
									   ('Cosméticos y Belleza'),('Bebidas Alcohólicas'),('Frutas y Verduras'),('Carnes y Embutidos'),('Panadería y Pastelería'),
									   ('Lácteos y Refrigerados'),('Accesorios de Cocina'),('Servicios y Recargas')
GO
CREATE TABLE T_Marcas(
IdMarca INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(100) UNIQUE
)
GO
INSERT INTO T_Marcas(Nombre) VALUES('N/A') 
GO
CREATE TABLE T_UnidadMedida(
IdUnidadMedida INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(50) UNIQUE
)
GO
INSERT INTO T_UnidadMedida(Nombre) VALUES('Unidad (und)'),('Caja (caja)'),('Paquete (paq)'),('Bolsa (bol)'),('Litro (lt)'),('Mililitro (ml)'),
									     ('Kilogramo (kg)'),('Gramo (g)'),('Metro (m)'),('Par (par)')
GO
CREATE TABLE T_Productos(
IdProducto INT IDENTITY(1,1) PRIMARY KEY,
Sku VARCHAR(50),
CodigoBarras VARCHAR(50),
Nombre VARCHAR(MAX),
CostoBase DECIMAL(10,2),
PrecioBase DECIMAL(10,2),
EsInventariable BIT NOT NULL,
Iva DECIMAL(10,2),
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
IdCategoria INT FOREIGN KEY REFERENCES T_Categorias(IdCategoria),
IdMarca INT FOREIGN KEY REFERENCES T_Marcas(IdMarca),
IdUnidadMedida INT FOREIGN KEY REFERENCES T_UnidadMedida(IdUnidadMedida),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
-- Índice único filtrado para SKU
CREATE UNIQUE INDEX UX_Productos_SKU
ON dbo.T_Productos(SKU)
WHERE SKU IS NOT NULL;
GO
-- Índice único filtrado para CodigoBarras
CREATE UNIQUE INDEX UX_Productos_CodigoBarras
ON dbo.T_Productos(CodigoBarras)
WHERE CodigoBarras IS NOT NULL;
GO
CREATE TABLE T_Proveedores (
IdProveedor INT IDENTITY(1,1) PRIMARY KEY,
IdIdentificationType INT,
NumeroDocumento VARCHAR(11) UNIQUE,
NombreRazonSocial VARCHAR(100) UNIQUE,
Direccion VARCHAR(150),
Telefono VARCHAR(20) UNIQUE,
Email VARCHAR(100),
Estado BIT,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY(IdIdentificationType) REFERENCES T_IdentificationType(IdIdentificationType),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
-- Índice único filtrado para Email
CREATE UNIQUE INDEX UX_Proveedores_Email
ON dbo.T_Proveedores(Email)
WHERE Email IS NOT NULL;
GO
INSERT INTO T_Proveedores (IdIdentificationType, NumeroDocumento, NombreRazonSocial, Estado, CreationDate, IdUserAction)
VALUES (1,'111','N/A',1,GETDATE(),1)
GO
CREATE TABLE T_ConversionesProducto (
IdProductoPadre INT,     -- Producto contenedor (ej. bolsa)
IdProductoHijo INT,      -- Producto contenido (ej. paquete)
Cantidad INT,            -- Cantidad de hijo en el padre
PRIMARY KEY (IdProductoPadre, IdProductoHijo),
FOREIGN KEY (IdProductoPadre) REFERENCES T_Productos(IdProducto),
FOREIGN KEY (IdProductoHijo) REFERENCES T_Productos(IdProducto)
)
GO
CREATE TABLE T_TipoCliente(
IdTipoCliente INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(50) UNIQUE
)
GO
INSERT INTO T_TipoCliente (Nombre)
VALUES('Minorista'),('Mayorista'),('Cliente Frecuente / VIP'),('Distribuidor'),('Institucional / Corporativo'),('N/A')
GO
CREATE TABLE T_Cliente (
IdCliente INT IDENTITY(1,1) PRIMARY KEY,
IdIdentificationType INT,
NumeroDocumento VARCHAR(11) UNIQUE,
NombreRazonSocial VARCHAR(100),
Direccion VARCHAR(150),
Telefono VARCHAR(20) UNIQUE,
Email VARCHAR(100),
IdTipoCliente INT,
Estado BIT,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY (IdTipoCliente) REFERENCES T_TipoCliente(IdTipoCliente),
FOREIGN KEY(IdIdentificationType) REFERENCES T_IdentificationType(IdIdentificationType),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
-- Índice único filtrado para Email
CREATE UNIQUE INDEX UX_Cliente_Email
ON dbo.T_Cliente(Email)
WHERE Email IS NOT NULL;
GO
INSERT INTO T_Cliente (IdIdentificationType, NumeroDocumento, NombreRazonSocial,IdTipoCliente,Estado,CreationDate,IdUserAction)
VALUES(1,'222','Consumidor final',1,1,GETDATE(),1)
GO
CREATE TABLE T_ListasPrecios (
IdListaPrecio INT IDENTITY(1,1) PRIMARY KEY,
NombreLista VARCHAR(100) UNIQUE,
IdTipoCliente INT,
FechaInicio DATE,
FechaFin DATE NULL,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY (IdTipoCliente) REFERENCES T_TipoCliente(IdTipoCliente),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
INSERT INTO T_ListasPrecios(NombreLista,IdTipoCliente) VALUES('N/A',6)
GO
CREATE TABLE T_PreciosProducto (
IdListaPrecio INT,
IdProducto INT,
Precio DECIMAL(10,2),
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
PRIMARY KEY (IdListaPrecio, IdProducto),
FOREIGN KEY (IdListaPrecio) REFERENCES T_ListasPrecios(IdListaPrecio),
FOREIGN KEY (IdProducto) REFERENCES T_Productos(IdProducto),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_PreciosCliente (
IdCliente INT,
IdProducto INT,
PrecioEspecial DECIMAL(10,2),
FechaInicio DATE,
FechaFin DATE,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
PRIMARY KEY (IdCliente, IdProducto),
FOREIGN KEY (IdCliente) REFERENCES T_Cliente(IdCliente),
FOREIGN KEY (IdProducto) REFERENCES T_Productos(IdProducto),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_TipoPromocion(
IdTipoPromocion INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(50) UNIQUE
)
GO
INSERT INTO T_TipoPromocion VALUES('Porcentaje') 
GO
CREATE TABLE T_Promociones (
IdPromocion INT IDENTITY(1,1) PRIMARY KEY,
NombrePromocion VARCHAR(100) UNIQUE,
IdTipoPromocion INT,       -- 'Porcentaje', 'ValorFijo', '2x1', etc.
Porcentaje DECIMAL(10,2),        -- Ej: 10% de descuento => 10.00
FechaInicio DATE,
FechaFin DATE,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser),
FOREIGN KEY(IdTipoPromocion) REFERENCES T_TipoPromocion(IdTipoPromocion)
)
GO
CREATE TABLE T_PromocionesProductos (
IdPromocion INT,
IdProducto INT,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
PRIMARY KEY (IdPromocion, IdProducto),
FOREIGN KEY (IdPromocion) REFERENCES T_Promociones(IdPromocion),
FOREIGN KEY (IdProducto) REFERENCES T_Productos(IdProducto),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
---Lógica de priorización (en consultas o lógica de negocio) 
---1. ¿Tiene precio personalizado? => usar PreciosCliente
---2. ¿Pertenece a una lista de precios por tipo de cliente? => usar PreciosProducto con ListasPrecios.TipoCliente = Clientes.TipoCliente
---3. ¿Hay promociones activas? => aplicar sobre el precio base
---4. Si nada aplica => usar PrecioBase por defecto si tienes uno (ej. en Productos)
GO
CREATE TABLE T_TipoEntrada(
IdTipoEntrada  INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(50) UNIQUE
)
GO
INSERT INTO T_TipoEntrada(Nombre) VALUES('Compra a proveedor'),('Devolución de cliente'),('Ajuste positivo')
GO
CREATE TABLE T_EntradasInventario (
IdEntradasInventario INT IDENTITY(1,1) PRIMARY KEY,
IdTipoEntrada INT,
IdProveedor INT NULL,
OrdenCompra VARCHAR(50),
NumFactura VARCHAR(50),
Comentario VARCHAR(255),
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY (IdProveedor) REFERENCES T_Proveedores(IdProveedor),
FOREIGN KEY (IdTipoEntrada) REFERENCES T_TipoEntrada(IdTipoEntrada)
)
GO
CREATE TABLE T_DetalleEntradasInventario (
IdDetalleEntradasInventario INT IDENTITY(1,1) PRIMARY KEY,
IdEntradasInventario INT,
IdProducto INT,
CodigoLote VARCHAR(20),
FechaVencimiento DATE,                
Cantidad DECIMAL(10,2) NOT NULL,
CostoUnitario DECIMAL(10,2) NOT NULL,
Descuento DECIMAL(10,2),
Iva DECIMAL(10,2),
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY (IdEntradasInventario) REFERENCES T_EntradasInventario(IdEntradasInventario),
FOREIGN KEY (IdProducto) REFERENCES T_Productos(IdProducto),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_TipoSalida(
IdTipoSalida  INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(50) UNIQUE
)
GO
INSERT INTO T_TipoSalida(Nombre) VALUES('Devolución a proveedor'),('Ajuste negativo'),('Consumo interno'),('Venta a cliente')
GO
CREATE TABLE T_SalidasInventario (
IdSalidasInventario INT IDENTITY(1,1) PRIMARY KEY,
IdTipoSalida INT,
IdProveedor INT NULL,
OrdenCompra VARCHAR(50),
NumFactura VARCHAR(50),
Comentario VARCHAR(255),
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY (IdProveedor) REFERENCES T_Proveedores(IdProveedor),
FOREIGN KEY (IdTipoSalida) REFERENCES T_TipoSalida(IdTipoSalida),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_DetalleSalidasInventario (
IdDetalleSalidasInventario INT IDENTITY(1,1) PRIMARY KEY,
IdSalidasInventario INT,
IdProducto INT,
CodigoLote VARCHAR(20),
FechaVencimiento DATE,
Cantidad DECIMAL(10,2) NOT NULL,
CostoUnitario DECIMAL(10,2) NOT NULL,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY (IdSalidasInventario) REFERENCES T_SalidasInventario(IdSalidasInventario),
FOREIGN KEY (IdProducto) REFERENCES T_Productos(IdProducto),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_Vendedor (
IdVendedor INT IDENTITY(1,1) PRIMARY KEY,
IdIdentificationType INT,
NumeroDocumento VARCHAR(11) UNIQUE,
Nombre VARCHAR(100),
Apellido VARCHAR(100),
Direccion VARCHAR(150),
Telefono VARCHAR(20) UNIQUE,
Email VARCHAR(100),
Estado BIT,
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY(IdIdentificationType) REFERENCES T_IdentificationType(IdIdentificationType),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
INSERT INTO T_Vendedor (IdIdentificationType,NumeroDocumento,Nombre, Estado)
VALUES(1,'333','Usuario Pos',1)
GO
CREATE TABLE T_Bancos (
    IdBanco INT IDENTITY(1,1) PRIMARY KEY,
    Codigo VARCHAR(10),
    Nombre NVARCHAR(100) NOT NULL,
    Estado BIT
)
GO
INSERT INTO T_Bancos (Nombre,Estado) 
VALUES
	('N/A',1),
	('Bancolombia',1),
	('Banco Mundo Mujer',1),
	('BBVA Colombia',1),
	('Banco AV Villas',1),
	('Banco Agrario',1),
	('Banco Caja Social BCSC SA',1),
	('Banco Davivienda SA',1),
	('Banco Falabella SA',1),
	('Banco Pichincha',1),
	('Banco Popular',1),
	('Banco W S.A.',1),
	('Banco de Bogota',1),
	('Banco de Occidente',1),
	('Banco CITIBACK',1),
	('Itau',1),
	('RappiPay',1),
	('Scotiabank Colpatria S.A.',1)
GO
CREATE TABLE T_MetodoPago (
IdMetodoPago INT IDENTITY(1,1) PRIMARY KEY,
Codigo VARCHAR(10) NOT NULL UNIQUE,
Nombre VARCHAR(50) NOT NULL,
RequiereReferencia BIT, -- Para tarjetas, transferencias
PermiteVuelto BIT, -- Solo efectivo
TieneComision BIT,
PorcentajeComision DECIMAL(5,2),
Activo BIT
)
GO
INSERT INTO T_MetodoPago 
VALUES
	('EF','Efectivo',0,1,0,0,1),
	('NQ','Nequi',1,0,1,0,1),
	('DV','DaviPlata',1,0,1,0,1),
	('BN','Bancolombia QR',1,0,1,0,1),
	('TR','Transferencia',1,0,0,0,1),
	('TC','Tarjeta Crédito',1,0,1,0,1),
	('TD','Tarjeta Débito',1,0,1,0,1)
GO
CREATE TABLE T_Ventas(
IdVenta INT IDENTITY(1,1) PRIMARY KEY,
Prefijo VARCHAR(5),
Consecutivo BIGINT,
IdCliente INT,
IdVendedor INT,
IdMetodoPago INT,
Estado VARCHAR(100), --FACTURADA, --ANULADA
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
IdUserActionNotaCredito INT,
FOREIGN KEY (IdCliente) REFERENCES T_Cliente(IdCliente),
FOREIGN KEY (IdVendedor) REFERENCES T_Vendedor(IdVendedor),
FOREIGN KEY (IdMetodoPago) REFERENCES T_MetodoPago(IdMetodoPago),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser),
FOREIGN KEY(IdUserActionNotaCredito) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_DetalleVenta(
IdDetalleVenta INT IDENTITY(1,1) PRIMARY KEY,
IdVenta INT,
IdProducto INT,
Sku VARCHAR(50),
CodigoBarras VARCHAR(50),
NombreProducto VARCHAR(MAX) NOT NULL,
Cantidad DECIMAL(10,2) NOT NULL,
UnidadMedida VARCHAR(50),
PrecioUnitario DECIMAL(10,2) NOT NULL,
CostoUnitario DECIMAL(10,2) NOT NULL,
Descuento DECIMAL(10,2),
Impuesto DECIMAL(10,2) NOT NULL,
CreationDate DATETIME,
IdUserAction INT,
FOREIGN KEY (IdVenta) REFERENCES T_Ventas(IdVenta),
FOREIGN KEY (IdProducto) REFERENCES T_Productos(IdProducto),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_PagosVenta (
IdPagoVenta INT IDENTITY(1,1) PRIMARY KEY,
IdVenta INT NOT NULL,
IdMetodoPago INT NOT NULL,
Recibido DECIMAL(10,2) NOT NULL,
Monto DECIMAL(10,2) NOT NULL,
Referencia VARCHAR(50) NULL, -- Número autorización, comprobante
IdBanco INT NOT NULL,
CreationDate DATETIME,
IdUserAction INT,
FOREIGN KEY (IdVenta) REFERENCES T_Ventas(IdVenta),
FOREIGN KEY (IdMetodoPago) REFERENCES T_MetodoPago(IdMetodoPago),
FOREIGN KEY (IdBanco) REFERENCES T_Bancos(IdBanco),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_Parametros(
IdParametro INT IDENTITY(1,1) PRIMARY KEY,
Nombre VARCHAR(100) NOT NULL,
Value VARCHAR(MAX) NOT NULL 
)
GO
INSERT INTO T_Parametros 
VALUES
	('Validar stock para venta','SI'),
    ('Preguntar imprimir factura en venta','NO'),
	('Buscar en venta por','Id'),
	('Tipo filtro producto','Inicia con'),
	('Ancho tirilla','42'),
	('Impresora','Generic'),
	('Ruta backup','')
GO
CREATE TABLE T_AperturaCierreCaja (
IdApertura_Cierre_caja INT IDENTITY(1,1) PRIMARY KEY,
IdUserAction INT,
FechaHoraApertura DATETIME NOT NULL,
MontoInicialDeclarado DECIMAL(10,2) NOT NULL,
FechaHoraCierre DATETIME NULL,
MontoFinalDeclarado DECIMAL(10,2) NULL,
VentasTotales DECIMAL(10,2) NULL,
PagosEnEfectivo  DECIMAL(10,2) NULL,
Diferencia DECIMAL(10,2) NULL,
Estado VARCHAR(20) NOT NULL, -- ABIERTA, CERRADA
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_NotaCredito (
IdNotaCredito INT PRIMARY KEY IDENTITY,
IdVenta INT,
Motivo NVARCHAR(255),
CreationDate DATETIME,
IdUserAction INT,
FOREIGN KEY (IdVenta) REFERENCES T_Ventas(IdVenta),
)
GO
CREATE TABLE T_NotaCreditoDetalle (
IdNotaCreditoDetalle INT PRIMARY KEY IDENTITY,
IdNotaCredito INT,
IdDetalleVenta INT,	
IdProducto INT,
Sku VARCHAR(50),
CodigoBarras VARCHAR(50),
NombreProducto VARCHAR(MAX) NOT NULL,
Cantidad DECIMAL(10,2) NOT NULL,
UnidadMedida VARCHAR(50),
PrecioUnitario DECIMAL(10,2) NOT NULL,
Descuento DECIMAL(10,2),
Impuesto DECIMAL(10,2) NOT NULL,
CreationDate DATETIME,
IdUserAction INT,
FOREIGN KEY (IdNotaCredito) REFERENCES T_NotaCredito(IdNotaCredito),
FOREIGN KEY (IdDetalleVenta) REFERENCES T_DetalleVenta(IdDetalleVenta),
FOREIGN KEY (IdProducto) REFERENCES T_Productos(IdProducto),
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser)
)
GO
CREATE TABLE T_Pagos (
IdPago INT PRIMARY KEY IDENTITY,
ValorPago DECIMAL(10,2),
Descripcion VARCHAR(100),
CreationDate DATETIME,
UpdateDate DATETIME,
IdUserAction INT,
FOREIGN KEY(IdUserAction) REFERENCES T_User(IdUser),
)