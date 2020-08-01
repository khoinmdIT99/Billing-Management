namespace Billing.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgentLedgerHeads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LedgerHead = c.String(maxLength: 255),
                        LedgerTypes = c.Int(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.LedgerHead, unique: true);
            
            CreateTable(
                "dbo.AgentLedgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgentLedgerHeadId = c.Int(nullable: false),
                        AgentId = c.Int(nullable: false),
                        GeneralLedgerId = c.Int(),
                        BankAccountLedgerId = c.Int(),
                        PaymentMethods = c.Int(),
                        PaymentType = c.Int(),
                        Amount = c.Double(nullable: false),
                        Balance = c.Double(nullable: false),
                        Remarks = c.String(),
                        SystemDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AgentLedgerHeads", t => t.AgentLedgerHeadId, cascadeDelete: true)
                .ForeignKey("dbo.Agents", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BankAccountLedgers", t => t.BankAccountLedgerId)
                .ForeignKey("dbo.GeneralLedgers", t => t.GeneralLedgerId)
                .Index(t => t.AgentLedgerHeadId)
                .Index(t => t.AgentId)
                .Index(t => t.GeneralLedgerId)
                .Index(t => t.BankAccountLedgerId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfileType = c.Int(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Postcode = c.String(),
                        Telephone = c.String(),
                        Mobile = c.String(),
                        FaxNo = c.String(),
                        Atol = c.String(),
                        CreditLimit = c.Double(nullable: false),
                        Balance = c.Double(nullable: false),
                        Remarks = c.String(),
                        JoiningDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PersonName = c.String(nullable: false),
                        MobileNo = c.String(maxLength: 20),
                        NationalId = c.String(maxLength: 50),
                        MaritialStatus = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        UserRoles = c.Int(nullable: false),
                        UserStatus = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.MobileNo, unique: true)
                .Index(t => t.NationalId, unique: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BankAccountLedgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankAccountId = c.Int(nullable: false),
                        BankAccountLedgerHeadId = c.Int(nullable: false),
                        LedgerTypes = c.Int(nullable: false),
                        PaymentMethods = c.Int(nullable: false),
                        GeneralLedgerId = c.Int(),
                        RelationId = c.Int(),
                        Notes = c.String(),
                        Amount = c.Double(nullable: false),
                        Balance = c.Double(nullable: false),
                        SysDateTime = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BankAccountLedgerHeads", t => t.BankAccountLedgerHeadId, cascadeDelete: true)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .ForeignKey("dbo.GeneralLedgers", t => t.GeneralLedgerId)
                .Index(t => t.BankAccountId)
                .Index(t => t.BankAccountLedgerHeadId)
                .Index(t => t.GeneralLedgerId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.BankAccountLedgerHeads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LedgerHead = c.String(maxLength: 100),
                        LedgerTypes = c.Int(nullable: false),
                        Editable = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        GeneralLedgerHeadId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GeneralLedgerHeads", t => t.GeneralLedgerHeadId)
                .Index(t => t.LedgerHead, unique: true, name: "IX_FirstAndSecond")
                .Index(t => t.GeneralLedgerHeadId);
            
            CreateTable(
                "dbo.GeneralLedgerHeads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GeneralLedgerHeadName = c.String(),
                        GeneralLedgerType = c.Int(nullable: false),
                        Editable = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankNames = c.Int(nullable: false),
                        AccountNo = c.String(),
                        AccountNames = c.String(),
                        Balance = c.Double(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.GeneralLedgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatementTypes = c.Int(nullable: false),
                        GeneralLedgerHeadId = c.Int(nullable: false),
                        PaymentMethods = c.Int(nullable: false),
                        GeneralLedgerType = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Notes = c.String(),
                        SysDateTime = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.GeneralLedgerHeads", t => t.GeneralLedgerHeadId, cascadeDelete: true)
                .Index(t => t.GeneralLedgerHeadId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Airlines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Code = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.AirportCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CashInHands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompanyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Telephone = c.String(),
                        FaxNo = c.String(),
                        Email = c.String(),
                        WebAddress = c.String(),
                        Atol = c.Double(nullable: false),
                        CustomerSafi = c.Double(nullable: false),
                        CreditCardCharge = c.Double(nullable: false),
                        DebitCardCharge = c.Double(nullable: false),
                        Apc = c.Double(nullable: false),
                        AgentSafi = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        Remarks = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        SysDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceType = c.Int(nullable: false),
                        AgentId = c.Int(nullable: false),
                        GdsBookingDate = c.String(),
                        AirlinesId = c.Int(nullable: false),
                        SysCreateDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        VendorId = c.Int(nullable: false),
                        VendorInvId = c.String(),
                        Pnr = c.String(),
                        ExpectedDatePayment = c.DateTime(),
                        GDSs = c.Int(nullable: false),
                        GDSUserId = c.String(),
                        CancellationChargeBefore = c.String(),
                        CancellationChargeAfter = c.String(),
                        CancellationDateBefore = c.String(),
                        CancellationDateAfter = c.String(),
                        NoShowBefore = c.String(),
                        NoShowAfter = c.String(),
                        InvoiceStatusS = c.Int(),
                        PaymentStatus = c.Int(),
                        ExtraCharge = c.Double(nullable: false),
                        PaidByAgent = c.Boolean(),
                        PaidToVendor = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agents", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("dbo.Airlines", t => t.AirlinesId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.AgentId)
                .Index(t => t.AirlinesId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.VendorId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Telephone = c.String(),
                        FaxNo = c.String(),
                        NetSafi = c.Double(nullable: false),
                        Atol = c.Double(nullable: false),
                        Balance = c.Double(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.InvoiceNames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        BookingDate = c.DateTime(),
                        PassengerTypes = c.Int(),
                        Name = c.String(),
                        TicketNo = c.String(),
                        Amount = c.Double(nullable: false),
                        CNetFare = c.Double(nullable: false),
                        VNetFare = c.Double(nullable: false),
                        TicketTax = c.Double(nullable: false),
                        VendorCharge = c.Double(nullable: false),
                        Apc = c.Double(nullable: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "dbo.InvoicePayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        AgentId = c.Int(nullable: false),
                        PaymentMethods = c.Int(nullable: false),
                        GeneralLedgerId = c.Int(nullable: false),
                        AgentLedgerId = c.Int(nullable: false),
                        BankAccountLedgerId = c.Int(),
                        Amount = c.Double(nullable: false),
                        Remarks = c.String(),
                        SysDateTime = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AgentLedgers", t => t.AgentLedgerId, cascadeDelete: true)
                .ForeignKey("dbo.Agents", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BankAccountLedgers", t => t.BankAccountLedgerId)
                .ForeignKey("dbo.GeneralLedgers", t => t.GeneralLedgerId, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.AgentId)
                .Index(t => t.GeneralLedgerId)
                .Index(t => t.AgentLedgerId)
                .Index(t => t.BankAccountLedgerId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.InvoicePaymentVendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                        PaymentMethods = c.Int(nullable: false),
                        GeneralLedgerId = c.Int(),
                        VendorLedgerId = c.Int(nullable: false),
                        BankAccountLedgerId = c.Int(),
                        Amount = c.Double(nullable: false),
                        Remarks = c.String(),
                        SysDateTime = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BankAccountLedgers", t => t.BankAccountLedgerId)
                .ForeignKey("dbo.GeneralLedgers", t => t.GeneralLedgerId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.VendorLedgers", t => t.VendorLedgerId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.VendorId)
                .Index(t => t.GeneralLedgerId)
                .Index(t => t.VendorLedgerId)
                .Index(t => t.BankAccountLedgerId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.VendorLedgers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorLedgerHeadId = c.Int(nullable: false),
                        GeneralLedgerId = c.Int(),
                        BankAccountLedgerId = c.Int(),
                        VendorId = c.Int(nullable: false),
                        PaymentMethods = c.Int(),
                        Amount = c.Double(nullable: false),
                        Balance = c.Double(nullable: false),
                        Remarks = c.String(),
                        SystemDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BankAccountLedgers", t => t.BankAccountLedgerId)
                .ForeignKey("dbo.GeneralLedgers", t => t.GeneralLedgerId)
                .ForeignKey("dbo.VendorLedgerHeads", t => t.VendorLedgerHeadId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorLedgerHeadId)
                .Index(t => t.GeneralLedgerId)
                .Index(t => t.BankAccountLedgerId)
                .Index(t => t.VendorId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.VendorLedgerHeads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LedgerHead = c.String(maxLength: 255),
                        LedgerTypes = c.Int(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.LedgerHead, unique: true);
            
            CreateTable(
                "dbo.InvoiceRefunds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RefundDate = c.DateTime(nullable: false),
                        VendorId = c.Int(nullable: false),
                        InvoiceId = c.Int(nullable: false),
                        ProfileName = c.String(),
                        TicketNo = c.String(),
                        CustomerFare = c.Double(nullable: false),
                        InvoiceTax = c.Double(nullable: false),
                        InvoiceNetFare = c.Double(nullable: false),
                        UserFare = c.Double(nullable: false),
                        CancellationFee = c.Double(nullable: false),
                        RefundTax = c.Double(nullable: false),
                        CustomerCancellationFee = c.Double(nullable: false),
                        VendorCharge = c.Double(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.InvoiceId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.InvoiceSegments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        AirlinesCode = c.String(),
                        FlightNo = c.String(),
                        SegmentClass = c.String(),
                        DepartureDate = c.String(),
                        DepartureFrom = c.String(),
                        DepartureTo = c.String(),
                        DepartureTime = c.String(),
                        ArrivalTime = c.String(),
                        SegmentStatus = c.String(),
                        SegmentSecondaryStatus = c.String(),
                        FlightDate = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "dbo.IPBPaymentDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BankAccountLedgerId = c.Int(),
                        BankAccountId = c.Int(nullable: false),
                        Remarks = c.String(),
                        BankDate = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BankAccountLedgers", t => t.BankAccountLedgerId)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .Index(t => t.BankAccountLedgerId)
                .Index(t => t.BankAccountId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.IPCCardDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(),
                        OtherInvoiceId = c.Int(),
                        InvoicePaymentId = c.Int(),
                        BankAccountId = c.Int(nullable: false),
                        CardNo = c.String(),
                        CardHolder = c.String(),
                        ExtraAmount = c.String(),
                        BankDate = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .ForeignKey("dbo.InvoicePayments", t => t.InvoicePaymentId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId)
                .ForeignKey("dbo.OtherInvoices", t => t.OtherInvoiceId)
                .Index(t => t.InvoiceId)
                .Index(t => t.OtherInvoiceId)
                .Index(t => t.InvoicePaymentId)
                .Index(t => t.BankAccountId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.OtherInvoices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgentId = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                        OtherInvoiceTypeId = c.Int(nullable: false),
                        Reference = c.String(),
                        ExpectedPayDate = c.String(),
                        VendorInvNo = c.String(),
                        Details = c.String(),
                        CustomerAgentAmount = c.Double(nullable: false),
                        VendorAmount = c.Double(nullable: false),
                        CustomerAgentPaid = c.Boolean(nullable: false),
                        VendorPaid = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agents", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.OtherInvoiceTypes", t => t.OtherInvoiceTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.AgentId)
                .Index(t => t.VendorId)
                .Index(t => t.OtherInvoiceTypeId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.OtherInvoiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceType = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.IPChequeDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(),
                        OtherInvoiceId = c.Int(),
                        InvoicePaymentId = c.Int(),
                        GeneralLedgerId = c.Int(),
                        BankNames = c.Int(nullable: false),
                        AccountNo = c.String(),
                        ChequeNo = c.String(),
                        SortCode = c.String(),
                        Amount = c.Double(nullable: false),
                        Remarks = c.String(),
                        SysCreateDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        Status = c.Int(nullable: false),
                        BulkPayment = c.Boolean(),
                        ProfileType = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.GeneralLedgers", t => t.GeneralLedgerId)
                .ForeignKey("dbo.InvoicePayments", t => t.InvoicePaymentId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId)
                .ForeignKey("dbo.OtherInvoices", t => t.OtherInvoiceId)
                .Index(t => t.InvoiceId)
                .Index(t => t.OtherInvoiceId)
                .Index(t => t.InvoicePaymentId)
                .Index(t => t.GeneralLedgerId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.IPDCardDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(),
                        OtherInvoiceId = c.Int(),
                        InvoicePaymentId = c.Int(),
                        BankAccountId = c.Int(nullable: false),
                        CardNo = c.String(),
                        CardHolder = c.String(),
                        ExtraAmount = c.String(),
                        BankDate = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .ForeignKey("dbo.InvoicePayments", t => t.InvoicePaymentId)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId)
                .ForeignKey("dbo.OtherInvoices", t => t.OtherInvoiceId)
                .Index(t => t.InvoiceId)
                .Index(t => t.OtherInvoiceId)
                .Index(t => t.InvoicePaymentId)
                .Index(t => t.BankAccountId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.OtherInvoiceLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OtherInvoiceId = c.Int(nullable: false),
                        Remarks = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        SysDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.OtherInvoices", t => t.OtherInvoiceId, cascadeDelete: true)
                .Index(t => t.OtherInvoiceId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.OtherInvoicePayments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OtherInvoiceId = c.Int(nullable: false),
                        AgentId = c.Int(nullable: false),
                        PaymentMethods = c.Int(nullable: false),
                        GeneralLedgerId = c.Int(nullable: false),
                        AgentLedgerId = c.Int(nullable: false),
                        BankAccountLedgerId = c.Int(),
                        Amount = c.Double(nullable: false),
                        Remarks = c.String(),
                        SysDateTime = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AgentLedgers", t => t.AgentLedgerId, cascadeDelete: true)
                .ForeignKey("dbo.Agents", t => t.AgentId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BankAccountLedgers", t => t.BankAccountLedgerId)
                .ForeignKey("dbo.GeneralLedgers", t => t.GeneralLedgerId, cascadeDelete: true)
                .ForeignKey("dbo.OtherInvoices", t => t.OtherInvoiceId, cascadeDelete: true)
                .Index(t => t.OtherInvoiceId)
                .Index(t => t.AgentId)
                .Index(t => t.GeneralLedgerId)
                .Index(t => t.AgentLedgerId)
                .Index(t => t.BankAccountLedgerId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.OtherInvoicePaymentVendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OtherInvoiceId = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                        PaymentMethods = c.Int(nullable: false),
                        GeneralLedgerId = c.Int(),
                        VendorLedgerId = c.Int(nullable: false),
                        BankAccountLedgerId = c.Int(),
                        Amount = c.Double(nullable: false),
                        Remarks = c.String(),
                        SysDateTime = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.BankAccountLedgers", t => t.BankAccountLedgerId)
                .ForeignKey("dbo.GeneralLedgers", t => t.GeneralLedgerId)
                .ForeignKey("dbo.OtherInvoices", t => t.OtherInvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.VendorLedgers", t => t.VendorLedgerId, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.OtherInvoiceId)
                .Index(t => t.VendorId)
                .Index(t => t.GeneralLedgerId)
                .Index(t => t.VendorLedgerId)
                .Index(t => t.BankAccountLedgerId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OtherInvoicePaymentVendors", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.OtherInvoicePaymentVendors", "VendorLedgerId", "dbo.VendorLedgers");
            DropForeignKey("dbo.OtherInvoicePaymentVendors", "OtherInvoiceId", "dbo.OtherInvoices");
            DropForeignKey("dbo.OtherInvoicePaymentVendors", "GeneralLedgerId", "dbo.GeneralLedgers");
            DropForeignKey("dbo.OtherInvoicePaymentVendors", "BankAccountLedgerId", "dbo.BankAccountLedgers");
            DropForeignKey("dbo.OtherInvoicePaymentVendors", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OtherInvoicePayments", "OtherInvoiceId", "dbo.OtherInvoices");
            DropForeignKey("dbo.OtherInvoicePayments", "GeneralLedgerId", "dbo.GeneralLedgers");
            DropForeignKey("dbo.OtherInvoicePayments", "BankAccountLedgerId", "dbo.BankAccountLedgers");
            DropForeignKey("dbo.OtherInvoicePayments", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OtherInvoicePayments", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.OtherInvoicePayments", "AgentLedgerId", "dbo.AgentLedgers");
            DropForeignKey("dbo.OtherInvoiceLogs", "OtherInvoiceId", "dbo.OtherInvoices");
            DropForeignKey("dbo.OtherInvoiceLogs", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.IPDCardDetails", "OtherInvoiceId", "dbo.OtherInvoices");
            DropForeignKey("dbo.IPDCardDetails", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.IPDCardDetails", "InvoicePaymentId", "dbo.InvoicePayments");
            DropForeignKey("dbo.IPDCardDetails", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.IPDCardDetails", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.IPChequeDetails", "OtherInvoiceId", "dbo.OtherInvoices");
            DropForeignKey("dbo.IPChequeDetails", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.IPChequeDetails", "InvoicePaymentId", "dbo.InvoicePayments");
            DropForeignKey("dbo.IPChequeDetails", "GeneralLedgerId", "dbo.GeneralLedgers");
            DropForeignKey("dbo.IPChequeDetails", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.IPCCardDetails", "OtherInvoiceId", "dbo.OtherInvoices");
            DropForeignKey("dbo.OtherInvoices", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.OtherInvoices", "OtherInvoiceTypeId", "dbo.OtherInvoiceTypes");
            DropForeignKey("dbo.OtherInvoiceTypes", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OtherInvoices", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OtherInvoices", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.IPCCardDetails", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.IPCCardDetails", "InvoicePaymentId", "dbo.InvoicePayments");
            DropForeignKey("dbo.IPCCardDetails", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.IPCCardDetails", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.IPBPaymentDetails", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.IPBPaymentDetails", "BankAccountLedgerId", "dbo.BankAccountLedgers");
            DropForeignKey("dbo.IPBPaymentDetails", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.InvoiceSegments", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.InvoiceRefunds", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.InvoiceRefunds", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.InvoiceRefunds", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.InvoicePaymentVendors", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.InvoicePaymentVendors", "VendorLedgerId", "dbo.VendorLedgers");
            DropForeignKey("dbo.VendorLedgers", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.VendorLedgers", "VendorLedgerHeadId", "dbo.VendorLedgerHeads");
            DropForeignKey("dbo.VendorLedgers", "GeneralLedgerId", "dbo.GeneralLedgers");
            DropForeignKey("dbo.VendorLedgers", "BankAccountLedgerId", "dbo.BankAccountLedgers");
            DropForeignKey("dbo.VendorLedgers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.InvoicePaymentVendors", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.InvoicePaymentVendors", "GeneralLedgerId", "dbo.GeneralLedgers");
            DropForeignKey("dbo.InvoicePaymentVendors", "BankAccountLedgerId", "dbo.BankAccountLedgers");
            DropForeignKey("dbo.InvoicePaymentVendors", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.InvoicePayments", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.InvoicePayments", "GeneralLedgerId", "dbo.GeneralLedgers");
            DropForeignKey("dbo.InvoicePayments", "BankAccountLedgerId", "dbo.BankAccountLedgers");
            DropForeignKey("dbo.InvoicePayments", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.InvoicePayments", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.InvoicePayments", "AgentLedgerId", "dbo.AgentLedgers");
            DropForeignKey("dbo.InvoiceNames", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.InvoiceLogs", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Vendors", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invoices", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invoices", "AirlinesId", "dbo.Airlines");
            DropForeignKey("dbo.Invoices", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.InvoiceLogs", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AgentLedgers", "GeneralLedgerId", "dbo.GeneralLedgers");
            DropForeignKey("dbo.AgentLedgers", "BankAccountLedgerId", "dbo.BankAccountLedgers");
            DropForeignKey("dbo.BankAccountLedgers", "GeneralLedgerId", "dbo.GeneralLedgers");
            DropForeignKey("dbo.GeneralLedgers", "GeneralLedgerHeadId", "dbo.GeneralLedgerHeads");
            DropForeignKey("dbo.GeneralLedgers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BankAccountLedgers", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.BankAccounts", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BankAccountLedgers", "BankAccountLedgerHeadId", "dbo.BankAccountLedgerHeads");
            DropForeignKey("dbo.BankAccountLedgerHeads", "GeneralLedgerHeadId", "dbo.GeneralLedgerHeads");
            DropForeignKey("dbo.BankAccountLedgers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AgentLedgers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AgentLedgers", "AgentId", "dbo.Agents");
            DropForeignKey("dbo.Agents", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AgentLedgers", "AgentLedgerHeadId", "dbo.AgentLedgerHeads");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OtherInvoicePaymentVendors", new[] { "ApplicationUserId" });
            DropIndex("dbo.OtherInvoicePaymentVendors", new[] { "BankAccountLedgerId" });
            DropIndex("dbo.OtherInvoicePaymentVendors", new[] { "VendorLedgerId" });
            DropIndex("dbo.OtherInvoicePaymentVendors", new[] { "GeneralLedgerId" });
            DropIndex("dbo.OtherInvoicePaymentVendors", new[] { "VendorId" });
            DropIndex("dbo.OtherInvoicePaymentVendors", new[] { "OtherInvoiceId" });
            DropIndex("dbo.OtherInvoicePayments", new[] { "ApplicationUserId" });
            DropIndex("dbo.OtherInvoicePayments", new[] { "BankAccountLedgerId" });
            DropIndex("dbo.OtherInvoicePayments", new[] { "AgentLedgerId" });
            DropIndex("dbo.OtherInvoicePayments", new[] { "GeneralLedgerId" });
            DropIndex("dbo.OtherInvoicePayments", new[] { "AgentId" });
            DropIndex("dbo.OtherInvoicePayments", new[] { "OtherInvoiceId" });
            DropIndex("dbo.OtherInvoiceLogs", new[] { "ApplicationUserId" });
            DropIndex("dbo.OtherInvoiceLogs", new[] { "OtherInvoiceId" });
            DropIndex("dbo.IPDCardDetails", new[] { "ApplicationUserId" });
            DropIndex("dbo.IPDCardDetails", new[] { "BankAccountId" });
            DropIndex("dbo.IPDCardDetails", new[] { "InvoicePaymentId" });
            DropIndex("dbo.IPDCardDetails", new[] { "OtherInvoiceId" });
            DropIndex("dbo.IPDCardDetails", new[] { "InvoiceId" });
            DropIndex("dbo.IPChequeDetails", new[] { "ApplicationUserId" });
            DropIndex("dbo.IPChequeDetails", new[] { "GeneralLedgerId" });
            DropIndex("dbo.IPChequeDetails", new[] { "InvoicePaymentId" });
            DropIndex("dbo.IPChequeDetails", new[] { "OtherInvoiceId" });
            DropIndex("dbo.IPChequeDetails", new[] { "InvoiceId" });
            DropIndex("dbo.OtherInvoiceTypes", new[] { "ApplicationUserId" });
            DropIndex("dbo.OtherInvoices", new[] { "ApplicationUserId" });
            DropIndex("dbo.OtherInvoices", new[] { "OtherInvoiceTypeId" });
            DropIndex("dbo.OtherInvoices", new[] { "VendorId" });
            DropIndex("dbo.OtherInvoices", new[] { "AgentId" });
            DropIndex("dbo.IPCCardDetails", new[] { "ApplicationUserId" });
            DropIndex("dbo.IPCCardDetails", new[] { "BankAccountId" });
            DropIndex("dbo.IPCCardDetails", new[] { "InvoicePaymentId" });
            DropIndex("dbo.IPCCardDetails", new[] { "OtherInvoiceId" });
            DropIndex("dbo.IPCCardDetails", new[] { "InvoiceId" });
            DropIndex("dbo.IPBPaymentDetails", new[] { "ApplicationUserId" });
            DropIndex("dbo.IPBPaymentDetails", new[] { "BankAccountId" });
            DropIndex("dbo.IPBPaymentDetails", new[] { "BankAccountLedgerId" });
            DropIndex("dbo.InvoiceSegments", new[] { "InvoiceId" });
            DropIndex("dbo.InvoiceRefunds", new[] { "ApplicationUserId" });
            DropIndex("dbo.InvoiceRefunds", new[] { "InvoiceId" });
            DropIndex("dbo.InvoiceRefunds", new[] { "VendorId" });
            DropIndex("dbo.VendorLedgerHeads", new[] { "LedgerHead" });
            DropIndex("dbo.VendorLedgers", new[] { "ApplicationUserId" });
            DropIndex("dbo.VendorLedgers", new[] { "VendorId" });
            DropIndex("dbo.VendorLedgers", new[] { "BankAccountLedgerId" });
            DropIndex("dbo.VendorLedgers", new[] { "GeneralLedgerId" });
            DropIndex("dbo.VendorLedgers", new[] { "VendorLedgerHeadId" });
            DropIndex("dbo.InvoicePaymentVendors", new[] { "ApplicationUserId" });
            DropIndex("dbo.InvoicePaymentVendors", new[] { "BankAccountLedgerId" });
            DropIndex("dbo.InvoicePaymentVendors", new[] { "VendorLedgerId" });
            DropIndex("dbo.InvoicePaymentVendors", new[] { "GeneralLedgerId" });
            DropIndex("dbo.InvoicePaymentVendors", new[] { "VendorId" });
            DropIndex("dbo.InvoicePaymentVendors", new[] { "InvoiceId" });
            DropIndex("dbo.InvoicePayments", new[] { "ApplicationUserId" });
            DropIndex("dbo.InvoicePayments", new[] { "BankAccountLedgerId" });
            DropIndex("dbo.InvoicePayments", new[] { "AgentLedgerId" });
            DropIndex("dbo.InvoicePayments", new[] { "GeneralLedgerId" });
            DropIndex("dbo.InvoicePayments", new[] { "AgentId" });
            DropIndex("dbo.InvoicePayments", new[] { "InvoiceId" });
            DropIndex("dbo.InvoiceNames", new[] { "InvoiceId" });
            DropIndex("dbo.Vendors", new[] { "ApplicationUserId" });
            DropIndex("dbo.Invoices", new[] { "VendorId" });
            DropIndex("dbo.Invoices", new[] { "ApplicationUserId" });
            DropIndex("dbo.Invoices", new[] { "AirlinesId" });
            DropIndex("dbo.Invoices", new[] { "AgentId" });
            DropIndex("dbo.InvoiceLogs", new[] { "ApplicationUserId" });
            DropIndex("dbo.InvoiceLogs", new[] { "InvoiceId" });
            DropIndex("dbo.Airlines", new[] { "Code" });
            DropIndex("dbo.Airlines", new[] { "Name" });
            DropIndex("dbo.GeneralLedgers", new[] { "ApplicationUserId" });
            DropIndex("dbo.GeneralLedgers", new[] { "GeneralLedgerHeadId" });
            DropIndex("dbo.BankAccounts", new[] { "ApplicationUserId" });
            DropIndex("dbo.BankAccountLedgerHeads", new[] { "GeneralLedgerHeadId" });
            DropIndex("dbo.BankAccountLedgerHeads", "IX_FirstAndSecond");
            DropIndex("dbo.BankAccountLedgers", new[] { "ApplicationUserId" });
            DropIndex("dbo.BankAccountLedgers", new[] { "GeneralLedgerId" });
            DropIndex("dbo.BankAccountLedgers", new[] { "BankAccountLedgerHeadId" });
            DropIndex("dbo.BankAccountLedgers", new[] { "BankAccountId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "NationalId" });
            DropIndex("dbo.AspNetUsers", new[] { "MobileNo" });
            DropIndex("dbo.Agents", new[] { "ApplicationUserId" });
            DropIndex("dbo.AgentLedgers", new[] { "ApplicationUserId" });
            DropIndex("dbo.AgentLedgers", new[] { "BankAccountLedgerId" });
            DropIndex("dbo.AgentLedgers", new[] { "GeneralLedgerId" });
            DropIndex("dbo.AgentLedgers", new[] { "AgentId" });
            DropIndex("dbo.AgentLedgers", new[] { "AgentLedgerHeadId" });
            DropIndex("dbo.AgentLedgerHeads", new[] { "LedgerHead" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OtherInvoicePaymentVendors");
            DropTable("dbo.OtherInvoicePayments");
            DropTable("dbo.OtherInvoiceLogs");
            DropTable("dbo.IPDCardDetails");
            DropTable("dbo.IPChequeDetails");
            DropTable("dbo.OtherInvoiceTypes");
            DropTable("dbo.OtherInvoices");
            DropTable("dbo.IPCCardDetails");
            DropTable("dbo.IPBPaymentDetails");
            DropTable("dbo.InvoiceSegments");
            DropTable("dbo.InvoiceRefunds");
            DropTable("dbo.VendorLedgerHeads");
            DropTable("dbo.VendorLedgers");
            DropTable("dbo.InvoicePaymentVendors");
            DropTable("dbo.InvoicePayments");
            DropTable("dbo.InvoiceNames");
            DropTable("dbo.Vendors");
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceLogs");
            DropTable("dbo.CompanyInfoes");
            DropTable("dbo.CashInHands");
            DropTable("dbo.AirportCodes");
            DropTable("dbo.Airlines");
            DropTable("dbo.GeneralLedgers");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.GeneralLedgerHeads");
            DropTable("dbo.BankAccountLedgerHeads");
            DropTable("dbo.BankAccountLedgers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Agents");
            DropTable("dbo.AgentLedgers");
            DropTable("dbo.AgentLedgerHeads");
        }
    }
}
