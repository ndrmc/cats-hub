namespace DRMFSS.Web.Reports
{
    partial class MasterReportBound
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterReportBound));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.rptSubReport = new DevExpress.XtraReports.UI.XRSubreport();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.ReportTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.HubName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportDate = new DevExpress.XtraReports.UI.XRLabel();
            this.PreparedBy = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.rptSubReport});
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // rptSubReport
            // 
            this.rptSubReport.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.rptSubReport.Name = "rptSubReport";
            this.rptSubReport.SizeF = new System.Drawing.SizeF(1380F, 100F);
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.ReportTitle,
            this.HubName,
            this.xrPictureBox1,
            this.xrPictureBox2});
            this.TopMargin.HeightF = 172F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportTitle
            // 
            this.ReportTitle.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReportTitle")});
            this.ReportTitle.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportTitle.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 149.625F);
            this.ReportTitle.Name = "ReportTitle";
            this.ReportTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ReportTitle.SizeF = new System.Drawing.SizeF(940.4166F, 22.20836F);
            this.ReportTitle.StylePriority.UseFont = false;
            this.ReportTitle.StylePriority.UseTextAlignment = false;
            this.ReportTitle.Text = "ReportTitle";
            this.ReportTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // HubName
            // 
            this.HubName.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "HubName")});
            this.HubName.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HubName.LocationFloat = new DevExpress.Utils.PointFloat(10.00001F, 126.625F);
            this.HubName.Name = "HubName";
            this.HubName.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.HubName.SizeF = new System.Drawing.SizeF(940.4166F, 23.00002F);
            this.HubName.StylePriority.UseFont = false;
            this.HubName.StylePriority.UseTextAlignment = false;
            this.HubName.Text = "HubName";
            this.HubName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(37.50022F, 10.00001F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(100F, 97.66666F);
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(137.5002F, 10.00001F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(502.4998F, 113.5F);
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrPageInfo1,
            this.xrLabel1,
            this.ReportDate,
            this.PreparedBy});
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(10.00006F, 57.29166F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "Report Date";
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Format = "Page{0} of {1}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(1190F, 57.29167F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(100F, 23F);
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(10.00006F, 19.70834F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Prepared by";
            // 
            // ReportDate
            // 
            this.ReportDate.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "ReportDate")});
            this.ReportDate.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Underline);
            this.ReportDate.LocationFloat = new DevExpress.Utils.PointFloat(122.9168F, 57.29166F);
            this.ReportDate.Name = "ReportDate";
            this.ReportDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.ReportDate.SizeF = new System.Drawing.SizeF(229.1667F, 23F);
            this.ReportDate.StylePriority.UseFont = false;
            this.ReportDate.Text = "ReportDate";
            // 
            // PreparedBy
            // 
            this.PreparedBy.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PreparedBy")});
            this.PreparedBy.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Underline);
            this.PreparedBy.LocationFloat = new DevExpress.Utils.PointFloat(122.9168F, 19.70834F);
            this.PreparedBy.Name = "PreparedBy";
            this.PreparedBy.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.PreparedBy.SizeF = new System.Drawing.SizeF(229.1667F, 23F);
            this.PreparedBy.StylePriority.UseFont = false;
            this.PreparedBy.Text = "PreparedBy";
            // 
            // MasterReportBound
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 10, 172, 100);
            this.PageHeight = 927;
            this.PageWidth = 1500;
            this.PaperKind = System.Drawing.Printing.PaperKind.LegalExtra;
            this.Version = "11.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox2;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        public DevExpress.XtraReports.UI.XRLabel ReportTitle;
        public DevExpress.XtraReports.UI.XRLabel HubName;
        public DevExpress.XtraReports.UI.XRSubreport rptSubReport;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        public DevExpress.XtraReports.UI.XRLabel ReportDate;
        public DevExpress.XtraReports.UI.XRLabel PreparedBy;
    }
}
