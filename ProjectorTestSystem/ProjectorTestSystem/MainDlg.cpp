// MainDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ProjectorTestSystem.h"
#include "MainDlg.h"
#include "afxdialogex.h"
#include "CApplication.h"
#include "CFont0.h"
#include "CRange.h"
#include "CWorkbook.h"
#include "CWorkbooks.h"
#include "CWorksheet.h"
#include "CWorksheets.h"
#include "ProjectorTestSystemDlg.h"
#include "ResizeCtrl.h"
#include "DeleteSelectTip.h"

/*全局变量*/
CString ZhiDanNum, Body, SingleBody, MainNum, mWiredMac, mWiredLessMac,MainSelectSql;
extern CProjectorTestSystemDlg * ProjectorTestSystemDlg;
CMainDlg * MainDlg;
CWindowSizeMange Main;
// CMainDlg 对话框

IMPLEMENT_DYNAMIC(CMainDlg, CDialogEx)

CMainDlg::CMainDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CMainDlg::IDD, pParent)
	, m_MainBody(_T(""))
	, m_MainSingleBody(_T(""))
	, m_MainOpticalCode(_T(""))
	, m_MMainBoardNum(_T(""))
	, m_MainWiredMac(_T(""))
	, m_MainWiredlessMac(_T(""))
{

}

CMainDlg::~CMainDlg()
{
}

void CMainDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST1, m_MainList);
	DDX_Control(pDX, IDC_ZHIDAN, m_MainZhiDan);
	DDX_Text(pDX, IDC_ZHIDAN, m_MainBody);
	DDX_Text(pDX, IDC_BODY, m_MainSingleBody);
	DDX_Text(pDX, IDC_SINGLEBODY, m_MainOpticalCode);
	DDX_Text(pDX, IDC_MAINNUM, m_MMainBoardNum);
	DDX_Text(pDX, IDC_WIRDMAC, m_MainWiredMac);
	DDX_Text(pDX, IDC_WIREDLESSMAC, m_MainWiredlessMac);
	DDX_Control(pDX, IDC_DELETEALL, m_DeleteAll);
	DDX_Control(pDX, IDC_DELETESELECT, m_DeleteSelect);
}


BEGIN_MESSAGE_MAP(CMainDlg, CDialogEx)
	ON_BN_CLICKED(IDC_CHECKINDB, &CMainDlg::OnBnClickedCheckindb)
	ON_BN_CLICKED(IDC_WRITETOEXCEL, &CMainDlg::OnBnClickedWritetoexcel)
	ON_BN_CLICKED(IDC_DELETESELECT, &CMainDlg::OnBnClickedDeleteselect)
	ON_BN_CLICKED(IDC_DELETEALL, &CMainDlg::OnBnClickedDeleteall)
	ON_WM_SIZE()
END_MESSAGE_MAP()


// CMainDlg 消息处理程序


BOOL CMainDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// TODO:  在此添加额外的初始化
	/*窗口大小*/
	CRect DlgRect;
	GetClientRect(&DlgRect);     //取客户区大小    
	Mainold.x = DlgRect.right - DlgRect.left;
	Mainold.y = DlgRect.bottom - DlgRect.top;
	/*list*/
	CRect ListRect;	
	// 获取编程语言列表视图控件的位置和大小   
	m_MainList.GetClientRect(&ListRect);
	// 为列表视图控件添加全行选中和栅格风格   
	m_MainList.SetExtendedStyle(m_MainList.GetExtendedStyle() | LVS_EX_FULLROWSELECT | LVS_EX_GRIDLINES);
	// 为列表视图控件添加三列   
	m_MainList.InsertColumn(0, _T("订单号"), LVCFMT_CENTER, 60, 0);
	m_MainList.InsertColumn(1, _T("机身码"), LVCFMT_CENTER, 60, 1);
	m_MainList.InsertColumn(2, _T("光机编码"), LVCFMT_CENTER, 60, 2);
	m_MainList.InsertColumn(3, _T("打光机作业时间"), LVCFMT_CENTER, 105, 3);
	m_MainList.InsertColumn(4, _T("主板编码"), LVCFMT_CENTER, 60, 4);
	m_MainList.InsertColumn(5, _T("打主板作业时间"), LVCFMT_CENTER,105, 5);
	m_MainList.InsertColumn(6, _T("老化前第一次测试时间"), LVCFMT_CENTER, 150, 6);
	m_MainList.InsertColumn(7, _T("老化前第二次测试时间"), LVCFMT_CENTER,150, 7);
	m_MainList.InsertColumn(8, _T("老化上架时间"), LVCFMT_CENTER, 90, 8);
	m_MainList.InsertColumn(9, _T("老化下架时间"), LVCFMT_CENTER, 90, 9);
	m_MainList.InsertColumn(10, _T("老化后第一次测试时间"), LVCFMT_CENTER, 150, 10);
	m_MainList.InsertColumn(11, _T("老化后第二次测试时间"), LVCFMT_CENTER, 150, 11);
	m_MainList.InsertColumn(12, _T("亮度测试前时间"), LVCFMT_CENTER, 105, 12);
	m_MainList.InsertColumn(13, _T("照度值"), LVCFMT_CENTER, 60, 13);
	m_MainList.InsertColumn(14, _T("有线MAC"), LVCFMT_CENTER,60, 14);
	m_MainList.InsertColumn(15, _T("无线MAC"), LVCFMT_CENTER, 60, 15);
	m_MainList.InsertColumn(16, _T("亮度测试时间"), LVCFMT_CENTER, 90, 16);
	m_MainList.InsertColumn(17, _T("维修描述"), LVCFMT_CENTER, 60, 17);
	m_MainList.InsertColumn(18, _T("维修后光机编码"), LVCFMT_CENTER,105, 18);
	m_MainList.InsertColumn(19, _T("维修后主板编码"), LVCFMT_CENTER,105, 19);
	m_MainList.InsertColumn(20, _T("维修时间"), LVCFMT_CENTER,60, 20);
	m_MainList.InsertColumn(21, _T("包装时间"), LVCFMT_CENTER, 60, 21);
	m_MainList.InsertItem(0, NULL);  //为了显示水平滚动条
	MainDlg = this;
	Main.Init(m_hWnd);
	return TRUE;  // return TRUE unless you set the focus to a control
	// 异常:  OCX 属性页应返回 FALSE
}

/*查询按钮*/
void CMainDlg::OnBnClickedCheckindb()
{
	// TODO:  在此添加控件通知处理程序代码
	CString ZhiDan, FuselageCode;
	CString IlluminationValue, WiredMAC, wirelessMAC, RepairText, AfterMaintenanceOpticalCode, AfterMaintenanceMainBoardCode, MainBoardCode;
	_variant_t PloMachineTime, PreAgingTestTime, AgeingBeginTime,AgeingEndTime,PostAgingTestTime,PreAgingTestTime2,PostAgingTestTime2;
	_variant_t RepairTime, PackingTime, LuminanceTestTime, LuminanceTestQTime, MainBoardTime;
	CString OpticalCode;
	CString IlluminationValueStr, WiredMACStr, wirelessMACStr, RepairTextStr, AfterMaintenanceOpticalCodeStr, AfterMaintenanceMainBoardCodeStr, MainBoardCodeStr;
	CString PloMachineTimeStr, PreAgingTestTimeStr, AgeingBeginTimeStr, AgeingEndTimeStr, PostAgingTestTimeStr, PreAgingTestTime2Str, PostAgingTestTime2Str;
	CString OpticalCodeStr;
	CString SelectSql,TermZhiDan,TermBody,TermSingleBody,TermMainBoard,TermWired,TermWiredLess;
	int RowNum = 0;
	CString RepairTimeStr, PackingTimeStr, LuminanceTestTimeStr, LuminanceTestQTimeStr, MainBoardTimeStr;
	m_MainList.DeleteAllItems();
	GetDlgItemText(IDC_ZHIDAN, ZhiDanNum);
	GetDlgItemText(IDC_BODY, Body);
	GetDlgItemText(IDC_SINGLEBODY, SingleBody);
	GetDlgItemText(IDC_MAINNUM, MainNum);
	GetDlgItemText(IDC_WIRDMAC, mWiredMac);
	GetDlgItemText(IDC_WIREDLESSMAC, mWiredLessMac);
	if (ZhiDanNum == ""&&Body == ""&&SingleBody == ""&&MainNum == ""&&mWiredLessMac == ""&&mWiredMac == "")
	{
		MessageBox(_T("请输入查询条件"), _T("提示"));
		return;
	}
	if (ZhiDanNum != "")
	{
		if (Body != "" || SingleBody != "" || MainNum != "" || mWiredMac != "" || mWiredLessMac != "")
		    TermZhiDan.Format(_T("ZhiDan = '%s' and "), ZhiDanNum);
		else
			TermZhiDan.Format(_T("ZhiDan = '%s'"), ZhiDanNum);
	}
	else
	{
		TermZhiDan = "";
	}

	if (Body != "")
	{
		if (SingleBody != "" || MainNum != "" || mWiredMac != "" || mWiredLessMac != "")
		    TermBody.Format(_T("FuselageCode = '%s' and "), Body);
		else
			TermBody.Format(_T("FuselageCode = '%s'"), Body);
	}
	else
	{
		TermBody = "";
	}
	
	if (SingleBody != "")
	{
		if (MainNum != "" || mWiredMac != "" || mWiredLessMac != "")
		    TermSingleBody.Format(_T("OpticalCode = '%s' and "), SingleBody);
		else
			TermSingleBody.Format(_T("OpticalCode = '%s'"), SingleBody);
	}
	else
	{
		TermSingleBody = "";
	}
	if (MainNum != "")
	{
		if (mWiredMac != "" || mWiredLessMac != "")
		    TermMainBoard.Format(_T("MainBoardCode = '%s' and "), MainNum);
		else
			TermMainBoard.Format(_T("MainBoardCode = '%s'"), MainNum);
	}
	else
	{
		TermMainBoard = "";
	}
	if (mWiredMac != "")
	{
		if (mWiredLessMac != "")
			TermWired.Format(_T("WiredMAC = '%s' and "), mWiredMac);
		else
			TermWired.Format(_T("WiredMAC = '%s'"), mWiredMac);
	}
	else
	{
		TermWired = "";
	}
	if (mWiredLessMac != "")
		TermWiredLess.Format(_T("wirelessMAC = '%s'"), mWiredLessMac);
	SelectSql = _T("select * from ProjectorInformation_MainTable where ") + TermZhiDan + TermBody + TermSingleBody + TermMainBoard + TermWired + TermWiredLess;
	OperateDB.OpenRecordset(SelectSql);
		try
		{
			if (!OperateDB.m_pRecordset->BOF)
				OperateDB.m_pRecordset->MoveFirst();
			else
			{
				AfxMessageBox(_T("数据库中没有与该条件匹配的数据"));
				OperateDB.CloseRecordset();
				return;
			}
			while (!OperateDB.m_pRecordset->adoEOF)
			{
				FuselageCode = OperateDB.m_pRecordset->GetCollect(_T("FuselageCode"));//机身码
				OpticalCode = CheckNull( OperateDB.m_pRecordset->GetCollect(_T("OpticalCode")));//光机码
				PreAgingTestTime = OperateDB.m_pRecordset->GetCollect(_T("PreAgingTestTime"));//老化前第一次测试
				IlluminationValue = CheckNull(OperateDB.m_pRecordset->GetCollect(_T("IlluminationValue")));//照度值
				WiredMAC = CheckNull(OperateDB.m_pRecordset->GetCollect(_T("WiredMAC")));//有线mac
				wirelessMAC = CheckNull( OperateDB.m_pRecordset->GetCollect(_T("wirelessMAC")));//无线mac
				RepairText = CheckNull(OperateDB.m_pRecordset->GetCollect(_T("RepairText")));//维修描述
				ZhiDan = OperateDB.m_pRecordset->GetCollect(_T("ZhiDan"));//订单号
				AfterMaintenanceOpticalCode = CheckNull( OperateDB.m_pRecordset->GetCollect(_T("AfterMaintenanceOpticalCode")));//维修后光机码
				AfterMaintenanceMainBoardCode = CheckNull(OperateDB.m_pRecordset->GetCollect(_T("AfterMaintenanceMainBoardCode")));//维修后主板码
				PloMachineTime = OperateDB.m_pRecordset->GetCollect(_T("PolishingMachineTime"));//打光机时间
				AgeingBeginTime = OperateDB.m_pRecordset->GetCollect(_T("AgeingBeginTime"));//老化上架
				AgeingEndTime = OperateDB.m_pRecordset->GetCollect(_T("AgeingEndTime"));//老化下架
				PostAgingTestTime = OperateDB.m_pRecordset->GetCollect(_T("PostAgingTestTime"));//老化后第一次测试
				PreAgingTestTime2 = OperateDB.m_pRecordset->GetCollect(_T("PreAgingTestTime2"));//老化前第二次测试
				PostAgingTestTime2 = OperateDB.m_pRecordset->GetCollect(_T("PostAgingTestTime2"));//老化后第二次测试
				RepairTime = OperateDB.m_pRecordset->GetCollect(_T("RepairTime"));//维修时间
				PackingTime = OperateDB.m_pRecordset->GetCollect(_T("PackingTime"));//包装时间
				LuminanceTestTime = OperateDB.m_pRecordset->GetCollect(_T("LuminanceTestTime"));//亮度测试时间
				LuminanceTestQTime = OperateDB.m_pRecordset->GetCollect(_T("LuminanceTestQTime"));//亮度测试前时间
				MainBoardCode = CheckNull(OperateDB.m_pRecordset->GetCollect(_T("MainBoardCode")));//主板编码
				MainBoardTime = OperateDB.m_pRecordset->GetCollect(_T("MainBoardTime"));//打主板时间
				MainBoardTimeStr = TimeTranSlate(MainBoardTime);
				LuminanceTestQTimeStr = TimeTranSlate(LuminanceTestQTime);
				LuminanceTestTimeStr = TimeTranSlate(LuminanceTestTime);
				PackingTimeStr = TimeTranSlate(PackingTime);
				RepairTimeStr = TimeTranSlate(RepairTime);
				PostAgingTestTime2Str = TimeTranSlate(PostAgingTestTime2);
				PreAgingTestTime2Str = TimeTranSlate(PreAgingTestTime2);
				PostAgingTestTimeStr = TimeTranSlate(PostAgingTestTime);
				AgeingEndTimeStr = TimeTranSlate(AgeingEndTime);
				AgeingBeginTimeStr = TimeTranSlate(AgeingBeginTime);
				PreAgingTestTimeStr = TimeTranSlate(PreAgingTestTime);
				PloMachineTimeStr = TimeTranSlate(PloMachineTime);
				m_MainList.InsertItem(RowNum, ZhiDan);
				m_MainList.SetItemText(RowNum, 1, FuselageCode);
				m_MainList.SetItemText(RowNum, 2, OpticalCode);
				m_MainList.SetItemText(RowNum, 3, PloMachineTimeStr);
				m_MainList.SetItemText(RowNum, 4, MainBoardCode);
				m_MainList.SetItemText(RowNum, 5, MainBoardTimeStr);
				m_MainList.SetItemText(RowNum, 6, PreAgingTestTimeStr);
				m_MainList.SetItemText(RowNum, 7, PreAgingTestTime2Str);
				m_MainList.SetItemText(RowNum, 8, AgeingBeginTimeStr);
				m_MainList.SetItemText(RowNum, 9, AgeingEndTimeStr);
				m_MainList.SetItemText(RowNum, 10, PostAgingTestTimeStr);
				m_MainList.SetItemText(RowNum, 11, PostAgingTestTime2Str);
				m_MainList.SetItemText(RowNum, 12, LuminanceTestQTimeStr);
				m_MainList.SetItemText(RowNum, 13, IlluminationValue);
				m_MainList.SetItemText(RowNum, 14, WiredMAC);
				m_MainList.SetItemText(RowNum, 15, wirelessMAC);
				m_MainList.SetItemText(RowNum, 16, LuminanceTestTimeStr);
				m_MainList.SetItemText(RowNum, 17, RepairText);
				m_MainList.SetItemText(RowNum, 18, AfterMaintenanceOpticalCode);
				m_MainList.SetItemText(RowNum, 19, AfterMaintenanceMainBoardCode);
				m_MainList.SetItemText(RowNum, 20, RepairTimeStr);
				m_MainList.SetItemText(RowNum, 21, PackingTimeStr);
				OperateDB.m_pRecordset->MoveNext();
				RowNum++;
			}
			OperateDB.CloseRecordset();
			m_DeleteAll.EnableWindow(TRUE);
			m_DeleteSelect.EnableWindow(TRUE);
		}
		catch (_com_error &e)
		{
			OperateDB.m_szErrorMsg = e.ErrorMessage();
			return;
		}
}

/*回车键响应函数*/
BOOL CMainDlg::PreTranslateMessage(MSG* pMsg)
{
	// TODO:  在此添加专用代码和/或调用基类

	if (pMsg->message == WM_KEYDOWN && pMsg->wParam == VK_RETURN)
	{
		if (GetFocus()->GetDlgCtrlID() == IDC_ZHIDAN)
		{
			OnBnClickedCheckindb();
		}
		if (GetFocus()->GetDlgCtrlID() == IDC_BODY)
		{
			OnBnClickedCheckindb();
		}
		if (GetFocus()->GetDlgCtrlID() == IDC_SINGLEBODY)
		{
			OnBnClickedCheckindb();
		}
		if (GetFocus()->GetDlgCtrlID() == IDC_MAINNUM)
		{
			OnBnClickedCheckindb();
		}
		if (GetFocus()->GetDlgCtrlID() == IDC_WIRDMAC)
		{
			OnBnClickedCheckindb();
		}
		if (GetFocus()->GetDlgCtrlID() == IDC_WIREDLESSMAC)
		{
			OnBnClickedCheckindb();
		}
	}
	return CDialogEx::PreTranslateMessage(pMsg);
}

/*导出按钮*/
void CMainDlg::OnBnClickedWritetoexcel()
{
	// TODO:  在此添加控件通知处理程序代码
	WriteToExcelFunc();
}

/*把list导出到excel的函数*/
void CMainDlg::WriteToExcelFunc()
{
	TCHAR szFilter[] = _T("文本文件 | *.xls; *.xlsx");
	CFileDialog fileDlg(TRUE, _T("txt"), NULL, 0, szFilter, this);
	CString strFilePath;
	if (IDOK == fileDlg.DoModal())
	{
		strFilePath = fileDlg.GetPathName();
	}
	else
	{
		return;
	}
	COleVariant
	covTrue((short)TRUE),
	covFalse((short)FALSE),
	covOptional((long)DISP_E_PARAMNOTFOUND, VT_ERROR);
	CApplication   app;
	CWorkbooks   books;
	CWorkbook   book;
	CWorksheets   sheets;
	CWorksheet   sheet;
	CRange   range;
	CFont0   font;
	if (!app.CreateDispatch(_T("Excel.Application")))
	{
		MessageBox(_T("创建失败！"), _T("提示"));
		return;
	}
	books = app.get_Workbooks();
	book = books.Add(covOptional);
	sheets = book.get_Worksheets();
	sheet = sheets.get_Item(COleVariant((short)1));
	CHeaderCtrl   *pmyHeaderCtrl;
	pmyHeaderCtrl = m_MainList.GetHeaderCtrl();//此句取得CListCtrl控件的列表頭
	int   iRow, iCol;
	int   m_cols = pmyHeaderCtrl->GetItemCount();
	int   m_rows = m_MainList.GetItemCount();
	HDITEM   hdi;
	TCHAR     lpBuffer[256];
	bool       fFound = false;
	hdi.mask = HDI_TEXT;
	hdi.pszText = lpBuffer;
	hdi.cchTextMax = 256;
	CString   colname;
	CString strTemp;
	for (iCol = 0; iCol <m_cols; iCol++)//将列表的标题头写入EXCEL
	{
		GetCellName(1, iCol + 1, colname);
		range = sheet.get_Range(COleVariant(colname), COleVariant(colname));
		pmyHeaderCtrl->GetItem(iCol, &hdi);
		range.put_Value2(COleVariant(hdi.pszText));
		int   nWidth = m_MainList.GetColumnWidth(iCol) / 6;
		//得到第iCol+1列  
		range.AttachDispatch(range.get_Item(_variant_t((long)(iCol + 1)), vtMissing).pdispVal, true);
		//设置列宽 
		range.put_ColumnWidth(_variant_t((long)nWidth));
	}
	range = sheet.get_Range(COleVariant(_T("A1 ")), COleVariant(colname));
	range.put_RowHeight(_variant_t((long)50));//设置行的高度
	font = range.get_Font();
	font.put_Bold(covTrue);
	range.put_VerticalAlignment(COleVariant((short)-4108));//xlVAlignCenter   =   -4108
	COleSafeArray   saRet;
	DWORD   numElements[] = { m_rows, m_cols };       //5x2   element   array
	saRet.Create(VT_BSTR, 2, numElements);
	range = sheet.get_Range(COleVariant(_T("A2 ")), covOptional);
	range = range.get_Resize(COleVariant((short)m_rows), COleVariant((short)m_cols));
	long   index[2];
	range = sheet.get_Range(COleVariant(_T("A2 ")), covOptional);
	range = range.get_Resize(COleVariant((short)m_rows), COleVariant((short)m_cols));
	for (iRow = 1; iRow <= m_rows; iRow++)//将列表内容写入EXCEL
	{
		for (iCol = 1; iCol <= m_cols; iCol++)
		{
			index[0] = iRow - 1;
			index[1] = iCol - 1;
			CString   szTemp;
			szTemp = m_MainList.GetItemText(iRow - 1, iCol - 1);
			BSTR   bstr = szTemp.AllocSysString();
			saRet.PutElement(index, bstr);
			SysFreeString(bstr);
		}
	}
	range.put_Value2(COleVariant(saRet));
	saRet.Detach();
	book.SaveCopyAs(COleVariant(strFilePath));
	book.put_Saved(true);
	book.ReleaseDispatch();
	books.ReleaseDispatch();
	app.Quit();
	app.ReleaseDispatch();
	MessageBox(_T("导出成功！"), _T("提示"));
}

/*获取单元头*/
void  CMainDlg::GetCellName(int nRow, int nCol, CString &strName)

{
	int nSeed = nCol;
	CString strRow;
	char cCell = 'A' + nCol - 1;
	strName.Format(_T("%c"), cCell);
	strRow.Format(_T("%d "), nRow);
	strName += strRow;
}

/*删除选择项*/
void CMainDlg::OnBnClickedDeleteselect()
{
	// TODO:  在此添加控件通知处理程序代码
	CDeleteSelectTip DeleteSelectTipDlg;
	INT_PTR nRes;
	BOOL DeleteItemFinshFlag = FALSE;
	BOOL DeleteSqlFinshFlag = FALSE;
	CString UpdateToSql, InsertTosql,CheckSql;
	CString MFuselageCode, MOpticalCode, MPolishingMachineTime, MPreAgingTestTime, MAgeingBeginTime, MAgeingEndTime, MPostAgingTestTime;
	CString MPreAgingTestTime2, MPostAgingTestTime2, MIlluminationValue, MWiredMAC, MwirelessMAC, MLuminanceTestTime, MRepairText, MRepairTime;
	CString MPackingTime, MMainBoardCode, MMainBoardTime, MZhiDan, MAfterMaintenanceOpticalCode, MAfterMaintenanceMainBoardCode, MLuminanceTestQTime;
	POSITION pos;
	int BeSelectCount, ErgodicOne,Index,CheckCount;
	CString BeSelectBodyNum,DeleteSql;

	BeSelectCount = m_MainList.GetSelectedCount();
	if (BeSelectCount == 0)
	{
		MessageBox(_T("未选中数据"), _T("提示"));
	}
	else
	{   
		nRes = DeleteSelectTipDlg.DoModal();
		if (nRes==IDCANCEL)
		{
			m_MainList.SetItemState(-1, 0, LVIS_SELECTED);
			m_MainList.SetSelectionMark(-1);
			return;
		}
		pos = m_MainList.GetFirstSelectedItemPosition();
		for (ErgodicOne = m_MainList.GetItemCount() - 1; ErgodicOne >= 0; ErgodicOne--)
		{
			Index = m_MainList.GetNextSelectedItem(pos);			
			BeSelectBodyNum = m_MainList.GetItemText(Index, 1);//机身码1
			MOpticalCode = m_MainList.GetItemText(Index, 2);//光机编码2
			MPolishingMachineTime = m_MainList.GetItemText(Index, 3);//打光机时间3
			MPreAgingTestTime = m_MainList.GetItemText(Index, 6);//老化前第一次测试时间4
			MAgeingBeginTime = m_MainList.GetItemText(Index, 8);//老化上架5
			MAgeingEndTime = m_MainList.GetItemText(Index, 9);//老化下架6
			MPostAgingTestTime = m_MainList.GetItemText(Index, 10);//老化后第一次7
			MPreAgingTestTime2 = m_MainList.GetItemText(Index, 7);//老化前第二次8		
			MPostAgingTestTime2 = m_MainList.GetItemText(Index, 11);//老化后第二次9
			MIlluminationValue = m_MainList.GetItemText(Index, 13);//照度值10
			MWiredMAC = m_MainList.GetItemText(Index, 14);//有线mac11
			MwirelessMAC = m_MainList.GetItemText(Index, 15);//无线mac12
			MLuminanceTestTime = m_MainList.GetItemText(Index, 16);//亮度测试时间13
			MRepairText = m_MainList.GetItemText(Index, 17);//维修描述14
			MRepairTime = m_MainList.GetItemText(Index, 20);//维修时间15
			MPackingTime = m_MainList.GetItemText(Index, 21);//包装时间16
			MMainBoardCode = m_MainList.GetItemText(Index, 4);//主板编码17
			MMainBoardTime = m_MainList.GetItemText(Index, 5);//打主板时间18
			MZhiDan = m_MainList.GetItemText(Index, 0);//订单19
			MAfterMaintenanceOpticalCode = m_MainList.GetItemText(Index, 18);//维修后光机码20
			MAfterMaintenanceMainBoardCode = m_MainList.GetItemText(Index, 19);//维修后主板码21
			MLuminanceTestQTime = m_MainList.GetItemText(Index, 12);//亮度测试前22
			CheckSql.Format(_T("select * from ProjectorInformation_MainTable_BackUp where FuselageCode = '%s'"), BeSelectBodyNum);
			OperateDB.OpenRecordset(CheckSql);
			CheckCount = OperateDB.GetRecordCount();
			OperateDB.CloseRecordset();
			if (CheckCount==0)
			{
				if (BeSelectBodyNum!="")
				{
					InsertTosql.Format(_T("INSERT INTO ProjectorInformation_MainTable_BackUp VALUES('%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s')"), \
						BeSelectBodyNum, MOpticalCode, MPolishingMachineTime, MPreAgingTestTime, MAgeingBeginTime, MAgeingEndTime, MPostAgingTestTime, \
						MPreAgingTestTime2, MPostAgingTestTime2, MIlluminationValue, MWiredMAC, MwirelessMAC, MLuminanceTestTime, MRepairText, MRepairTime, MPackingTime, \
						MMainBoardCode, MMainBoardTime, MZhiDan, MAfterMaintenanceOpticalCode, MAfterMaintenanceMainBoardCode, MLuminanceTestQTime);
					OperateDB.ExecuteByConnection(InsertTosql);
				}				
			}
			else
			{
				UpdateToSql.Format(_T("UPDATE Person SET OpticalCode = '%s', PolishingMachineTime = '%s',PreAgingTestTime = '%s',AgeingBeginTime = '%s',\
									  AgeingEndTime = '%s',PostAgingTestTime = '%s',PreAgingTestTime2 = '%s',PostAgingTestTime2 = '%s',\
									  IlluminationValue = '%s',WiredMAC = '%s',wirelessMAC = '%s',LuminanceTestTime = '%s',\
									  RepairText = '%s',RepairTime = '%s',PackingTime = '%s',MainBoardCode = '%s',\
									  MainBoardTime = '%s',ZhiDan = '%s',AfterMaintenanceOpticalCode = '%s',AfterMaintenanceMainBoardCode = '%s',LuminanceTestQTime = '%s' WHERE FuselageCode = '%s'"),\
									  MOpticalCode, MPolishingMachineTime, MPreAgingTestTime, MAgeingBeginTime, MAgeingEndTime, MPostAgingTestTime, \
									  MPreAgingTestTime2, MPostAgingTestTime2, MIlluminationValue, MWiredMAC, MwirelessMAC, MLuminanceTestTime, MRepairText, MRepairTime, MPackingTime, \
									  MMainBoardCode, MMainBoardTime, MZhiDan, MAfterMaintenanceOpticalCode, MAfterMaintenanceMainBoardCode, MLuminanceTestQTime, BeSelectBodyNum);
				OperateDB.ExecuteByConnection(UpdateToSql);
			}				
			DeleteSql.Format(_T("DELETE FROM ProjectorInformation_MainTable WHERE FuselageCode = '%s'"), BeSelectBodyNum);		
			DeleteSqlFinshFlag = OperateDB.ExecuteByConnection(DeleteSql);
			if (m_MainList.GetItemState(ErgodicOne, LVIS_SELECTED) == LVIS_SELECTED)
				m_MainList.DeleteItem(ErgodicOne);
		}
			
	}
	if (DeleteSqlFinshFlag==TRUE)
	{
		MessageBox(_T("删除成功！"), _T("提示"));
	}
}

/*删除界面所有数据*/
void CMainDlg::OnBnClickedDeleteall()
{
	// TODO:  在此添加控件通知处理程序代码
	BOOL DeleteAllFinshFlag = FALSE;
	BOOL DeleteAllSqlFinshFlag = FALSE;
	int ItemCount,ErgodicAll,CheckCount,MyIDOK;
	CString UpdateSql, InsertSql,MyCheckSql;
	CString  SqlDeleteAll,DeleteAllBodyNum;
	CString AFuselageCode, AOpticalCode, APolishingMachineTime, APreAgingTestTime, AAgeingBeginTime, AAgeingEndTime, APostAgingTestTime;
	CString APreAgingTestTime2, APostAgingTestTime2, AIlluminationValue, AWiredMAC, AwirelessMAC, ALuminanceTestTime, ARepairText, ARepairTime;
	CString APackingTime, AMainBoardCode, AMainBoardTime, AZhiDan, AAfterMaintenanceOpticalCode, AAfterMaintenanceMainBoardCode, ALuminanceTestQTime;
	ItemCount = m_MainList.GetItemCount();
	if (ItemCount == 0)
	{
		MessageBox(_T("当前界面没有数据，删除失败！"), _T("提示"));
		return;
	}
	else
	{
		MyIDOK = MessageBox(_T("您确定要删除界面中的所有数据吗？"), _T("提示"), MB_OK | MB_OKCANCEL);
		if (MyIDOK==2)
		{
			return;
		}
		for (ErgodicAll = 0; ErgodicAll< ItemCount; ErgodicAll++)
		{
			DeleteAllBodyNum = m_MainList.GetItemText(ErgodicAll, 1);//光机码1
			AOpticalCode = m_MainList.GetItemText(ErgodicAll, 2);//光机编码2
			APolishingMachineTime = m_MainList.GetItemText(ErgodicAll, 3);//打光机时间3
			APreAgingTestTime = m_MainList.GetItemText(ErgodicAll, 6);//老化前第一次测试时间4
			AAgeingBeginTime = m_MainList.GetItemText(ErgodicAll, 8);//老化上架5
			AAgeingEndTime = m_MainList.GetItemText(ErgodicAll, 9);//老化下架6
			APostAgingTestTime = m_MainList.GetItemText(ErgodicAll, 10);//老化后第一次7
			APreAgingTestTime2 = m_MainList.GetItemText(ErgodicAll, 7);//老化前第二次8		
			APostAgingTestTime2 = m_MainList.GetItemText(ErgodicAll, 11);//老化后第二次9
			AIlluminationValue = m_MainList.GetItemText(ErgodicAll, 13);//照度值10
			AWiredMAC = m_MainList.GetItemText(ErgodicAll, 14);//有线mac11
			AwirelessMAC = m_MainList.GetItemText(ErgodicAll, 15);//无线mac12
			ALuminanceTestTime = m_MainList.GetItemText(ErgodicAll, 16);//亮度测试时间13
			ARepairText = m_MainList.GetItemText(ErgodicAll, 17);//维修描述14
			ARepairTime = m_MainList.GetItemText(ErgodicAll, 20);//维修时间15
			APackingTime = m_MainList.GetItemText(ErgodicAll, 21);//包装时间16
			AMainBoardCode = m_MainList.GetItemText(ErgodicAll, 4);//主板编码17
			AMainBoardTime = m_MainList.GetItemText(ErgodicAll, 5);//打主板时间18
			AZhiDan = m_MainList.GetItemText(ErgodicAll, 0);//订单19
			AAfterMaintenanceOpticalCode = m_MainList.GetItemText(ErgodicAll, 18);//维修后光机码20
			AAfterMaintenanceMainBoardCode = m_MainList.GetItemText(ErgodicAll, 19);//维修后主板码21
			ALuminanceTestQTime = m_MainList.GetItemText(ErgodicAll, 12);//亮度测试前22
			MyCheckSql.Format(_T("select * from ProjectorInformation_MainTable_BackUp where FuselageCode = '%s'"), DeleteAllBodyNum);
			OperateDB.OpenRecordset(MyCheckSql);
			CheckCount = OperateDB.GetRecordCount();
			OperateDB.CloseRecordset();
			if (CheckCount==0)
			{
				if (DeleteAllBodyNum!="")
				{
					InsertSql.Format(_T("INSERT INTO ProjectorInformation_MainTable_BackUp VALUES('%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s')"), \
						DeleteAllBodyNum, AOpticalCode, APolishingMachineTime, APreAgingTestTime, AAgeingBeginTime, AAgeingEndTime, APostAgingTestTime, \
						APreAgingTestTime2, APostAgingTestTime2, AIlluminationValue, AWiredMAC, AwirelessMAC, ALuminanceTestTime, ARepairText, ARepairTime, APackingTime, \
						AMainBoardCode, AMainBoardTime, AZhiDan, AAfterMaintenanceOpticalCode, AAfterMaintenanceMainBoardCode, ALuminanceTestQTime);
					OperateDB.ExecuteByConnection(InsertSql);
				}				
			}
			else
			{
				UpdateSql.Format(_T("UPDATE Person SET OpticalCode = '%s', PolishingMachineTime = '%s',PreAgingTestTime = '%s',AgeingBeginTime = '%s',\
								  AgeingEndTime = '%s',PostAgingTestTime = '%s',PreAgingTestTime2 = '%s',PostAgingTestTime2 = '%s',\
								 IlluminationValue = '%s',WiredMAC = '%s',wirelessMAC = '%s',LuminanceTestTime = '%s',\
								RepairText = '%s',RepairTime = '%s',PackingTime = '%s',MainBoardCode = '%s',\
								MainBoardTime = '%s',ZhiDan = '%s',AfterMaintenanceOpticalCode = '%s',AfterMaintenanceMainBoardCode = '%s',LuminanceTestQTime = '%s' WHERE FuselageCode = '%s'"), \
								AOpticalCode, APolishingMachineTime, APreAgingTestTime, AAgeingBeginTime, AAgeingEndTime, APostAgingTestTime, \
								APreAgingTestTime2, APostAgingTestTime2, AIlluminationValue, AWiredMAC, AwirelessMAC, ALuminanceTestTime, ARepairText, ARepairTime, APackingTime, \
								AMainBoardCode, AMainBoardTime, AZhiDan, AAfterMaintenanceOpticalCode, AAfterMaintenanceMainBoardCode, ALuminanceTestQTime, DeleteAllBodyNum);
			}
			SqlDeleteAll.Format(_T("DELETE FROM ProjectorInformation_MainTable WHERE FuselageCode = '%s'"), DeleteAllBodyNum);
			DeleteAllSqlFinshFlag = OperateDB.ExecuteByConnection(SqlDeleteAll);
		}
		DeleteAllFinshFlag = m_MainList.DeleteAllItems();
	}	
	if (DeleteAllFinshFlag == TRUE&&DeleteAllSqlFinshFlag==TRUE)
	{
		MessageBox(_T("删除成功！"), _T("提示"));
		m_DeleteAll.EnableWindow(FALSE);
	}
	else
	{
		MessageBox(_T("删除失败！"), _T("提示"));
	}
}

/*时间转换函数*/
CString CMainDlg::TimeTranSlate(_variant_t SourceTime)
{
	SYSTEMTIME MyTime;
	CString MyTimeStr;
	CString NULLStr = "";
	if (SourceTime.vt!=VT_NULL)
	{
		COleDateTime OleTime = (COleDateTime)SourceTime;
		VariantTimeToSystemTime(OleTime, &MyTime);
		MyTimeStr.Format(_T("%d-%d-%d  %d:%d:%d"), MyTime.wYear, MyTime.wMonth, MyTime.wDay, MyTime.wHour, MyTime.wMinute, MyTime.wSecond);
		return MyTimeStr;
	}
	else
	{
		return NULLStr;
	}

}

void CMainDlg::OnCancel()
{
	// TODO:  在此添加专用代码和/或调用基类

	//CDialogEx::OnCancel();
}


void CMainDlg::OnOK()
{
	// TODO:  在此添加专用代码和/或调用基类

	/*CDialogEx::OnOK();*/
}

/*根据比例改变控件位置和大小*/
void CMainDlg::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);

	// TODO:  在此处添加消息处理程序代码
	if (nType == SIZE_RESTORED || nType == SIZE_MAXIMIZED)
	{
		Main.ResizeWindow();
	}
}

/*查数据库NULL*/
CString CMainDlg::CheckNull(_variant_t Source)
{
	CString DestStr;
	if (Source.vt ==VT_NULL)
	{
		DestStr = "";
		return DestStr;
	}
	else
	{
		DestStr = Source.bstrVal;
		return DestStr;
	}
}