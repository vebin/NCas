/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     4/6 星期三 14:34:02                             */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('Account')
            and   type = 'U')
   drop table Account
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('AccountIndex')
            and   name  = 'Index_AccountCode'
            and   indid > 0
            and   indid < 255)
   drop index AccountIndex.Index_AccountCode
go

if exists (select 1
            from  sysobjects
           where  id = object_id('AccountIndex')
            and   type = 'U')
   drop table AccountIndex
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
/* Table: AccountIndex                                          */
/*==============================================================*/
create table AccountIndex (
   AccountId            varchar(36)          not null,
   Code                 nvarchar(50)         not null,
   constraint PK_ACCOUNTINDEX primary key (AccountId)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('AccountIndex') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'AccountIndex' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   '账号索引表', 
   'user', @CurrentUser, 'table', 'AccountIndex'
go

/*==============================================================*/
/* Index: Index_AccountCode                                     */
/*==============================================================*/
create unique index Index_AccountCode on AccountIndex (
Code ASC
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

