/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     4/7 星期四 17:27:58                             */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('Account')
            and   type = 'U')
   drop table Account
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('AccountCodeIndex')
            and   name  = 'Index_AccountCode'
            and   indid > 0
            and   indid < 255)
   drop index AccountCodeIndex.Index_AccountCode
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AccountCodeIndex')
            and   type = 'U')
   drop table AccountCodeIndex
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('AccountNameIndex')
            and   name  = 'Index_AccountName'
            and   indid > 0
            and   indid < 255)
   drop index AccountNameIndex.Index_AccountName
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AccountNameIndex')
            and   type = 'U')
   drop table AccountNameIndex
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WebApp')
            and   type = 'U')
   drop table WebApp
go

/*==============================================================*/
/* Table: Account                                               */
/*==============================================================*/
create table Account (
   AccountId            varchar(36)          not null,
   Code                 nvarchar(50)         not null,
   AccountName          nvarchar(50)         not null,
   Password             nvarchar(256)        not null,
   UseFlag              int                  not null,
   Version              bigint               not null,
   EventSequence        int                  not null,
   constraint PK_ACCOUNT primary key (AccountId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('Account') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'Account' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '账号表', 
   'user', @CurrentUser, 'table', 'Account'
go

/*==============================================================*/
/* Table: AccountCodeIndex                                      */
/*==============================================================*/
create table AccountCodeIndex (
   AccountId            varchar(36)          not null,
   Code                 nvarchar(50)         not null,
   constraint PK_ACCOUNTCODEINDEX primary key (AccountId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('AccountCodeIndex') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'AccountCodeIndex' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '账号索引表', 
   'user', @CurrentUser, 'table', 'AccountCodeIndex'
go

/*==============================================================*/
/* Index: Index_AccountCode                                     */
/*==============================================================*/
create unique index Index_AccountCode on AccountCodeIndex (
Code ASC
)
go

/*==============================================================*/
/* Table: AccountNameIndex                                      */
/*==============================================================*/
create table AccountNameIndex (
   AccountId            varchar(36)          not null,
   AccountName          nvarchar(50)         not null,
   constraint PK_ACCOUNTNAMEINDEX primary key (AccountId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('AccountNameIndex') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'AccountNameIndex' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '账号名索引表', 
   'user', @CurrentUser, 'table', 'AccountNameIndex'
go

/*==============================================================*/
/* Index: Index_AccountName                                     */
/*==============================================================*/
create unique index Index_AccountName on AccountNameIndex (
AccountName ASC
)
go

/*==============================================================*/
/* Table: WebApp                                                */
/*==============================================================*/
create table WebApp (
   WebAppId             varchar(36)          not null,
   AppKey               nvarchar(80)         not null,
   AppName              nvarchar(50)         not null,
   Url                  nvarchar(128)        not null,
   VerifyTicketUrl      nvarchar(128)        not null,
   NotifyUrl            nvarchar(128)        not null,
   UseFlag              int                  not null,
   Version              bigint               not null,
   EventSequence        int                  not null,
   constraint PK_WEBAPP primary key (WebAppId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('WebApp') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'WebApp' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '网站节点表', 
   'user', @CurrentUser, 'table', 'WebApp'
go

