using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Accounting.MasterData;

public class frmSubCostCenterTree : frmTree2
{
	private IContainer components = null;

	public frmSubCostCenterTree()
	{
		InitializeComponent();
		IDCol = "SubCostCenterID";
		NoCol = "SubCostCenterNumber";
		NameCol = "SubCostCenterNameAr";
		NameEnCol = "SubCostCenterNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		ItemLevelCol = "LevelID";
		TableName = "A_SubCostCenter";
		LevelsTable = "A_SubCostCenters_Levels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
		AllowAddRoot = true;
	}

	public override void PrepareData()
	{
		base.PrepareData();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		if (Adding)
		{
			((Control)(object)txtCode).Text = SubCostCenter.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			((Control)(object)txtName).Select();
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
	}

	public override void DisplayData()
	{
		base.DisplayData();
	}

	public override int TreeAddData()
	{
		int result = SubCostCenter.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), "Null", "Null", "Null", "Null", "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
		return result;
	}

	public override void TreeUpdateData()
	{
		SubCostCenter.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", NodeLevel.ToString(), "Null", "Null", "Null", "Null", "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	public override void TreeDeleteData()
	{
		SubCostCenterDetails.DeleteBySubCostCenterID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
		SubCostCenter.Delete(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
	}

	public override bool HasTransactionValidation()
	{
		if (JVDetails.SelectBySubCostCenterID(((KeyedSubObjectBase)SelectedNode).Key, "1").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("مركز التكلفه التحليلي مستخدم في القيود", "This Account Exists in JVs");
			return true;
		}
		if (RepeatedJVDetails.SelectBySubCostCenterID(((KeyedSubObjectBase)SelectedNode).Key, "1").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("مركز التكلفه التحليلي في القيود التكراريه", "This Account Exists in Repeated JV");
			return true;
		}
		return base.HasTransactionValidation();
	}

	public override void TreeSearch()
	{
		int num = SearchFunctions.SubCostCenter(IsFromServer: true);
		if (num != 0)
		{
			treeChart.CollapseAll();
			treeChart.ActiveNode = treeChart.GetNodeByKey(num.ToString());
			treeChart.GetNodeByKey(num.ToString()).Selected = true;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.MasterData.frmSubCostCenterTree));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.dtChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.treeChart, "treeChart");
		resources.ApplyResources(base.txtCode, "txtCode");
		((EditorButtonControlBase)base.txtCode).ReadOnly = true;
		resources.ApplyResources(base.txtName, "txtName");
		((EditorButtonControlBase)base.txtName).ReadOnly = true;
		resources.ApplyResources(base.lblPath, "lblPath");
		resources.ApplyResources(base.txtNameEn, "txtNameEn");
		((EditorButtonControlBase)base.txtNameEn).ReadOnly = true;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData";
		((ControlBase)base.label1).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.label1, "label1");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		((SubObjectBase)val2).ForceApplyResources = "FontData";
		((ControlBase)base.label2).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(base.label2, "label2");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		((SubObjectBase)val3).ForceApplyResources = "FontData";
		((ControlBase)base.label3).Appearance = (AppearanceBase)(object)val3;
		resources.ApplyResources(base.label3, "label3");
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Name = "frmSubCostCenterTree";
		((System.ComponentModel.ISupportInitialize)base.dtChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
