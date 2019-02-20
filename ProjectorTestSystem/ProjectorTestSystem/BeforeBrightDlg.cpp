// BeforeBrightDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ProjectorTestSystem.h"
#include "BeforeBrightDlg.h"
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


/*全局变量*/
int BrightFirstRow = 0;
CBeforeBrightDlg *BeforeBrightDlg;
CWindowSizeMange BeforeBright;
extern CProjectorTestSystemDlg * ProjectorTestSystemDlg;
// CBeforeBrightDlg 对话框

IMPLEMENT_DYNAMIC(CBeforeBrightDlg, CDialogEx)

CBeforeBrightDlg::CBeforeBrightDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CBeforeBrightDlg::IDD, pParent)
	, m_BrightBodyEditVal(_T(""))
	, m_BrightNessEditVal(_T(""))
	, m_WiredLessEditVal(_T(""))
	, m_WiredLessEditState(FALSE)
	, m_WiredEditVal(_T(""))
	, m_WiredEditState(FALSE)
	, m_BrightStaticVal(_T(""))
{

}

CBeforeBrightDlg::~CBeforeBrightDlg()
{
}

void CBeforeBrightDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST1, m_BeforeBright);
	DDX_Control(pDX, IDC_BEFOREBRIGHT_STATIC, m_BeforeBrightNum);
	DDX_Control(pDX, IDC_BRIGHT_BODY, m_BrightBodyEdit);
	DDX_Control(pDX, IDC_BRIGHTNESS, m_BrightNessEdit);
	DDX_Text(pDX, IDC_BRIGHT_BODY, m_BrightBodyEditVal);
	DDX_Text(pDX, IDC_BRIGHTNESS, m_BrightNessEditVal);
	DDX_Control(pDX, IDC_WIREDLESSMAC, m_WiredLessEdit);
	DDX_Text(pDX, IDC_WIREDLESSMAC, m_WiredLessEditVal);
	DDX_Check(pDX, IDC_CHECK2, m_WiredLessEditState);
	DDX_Control(pDX, IDC_WIREDMAC, m_WiredEdit);
	DDX_Text(pDX, IDC_WIREDMAC, m_WiredEditVal);
	DDX_Check(pDX, IDC_CHECK3, m_WiredEditState);
	DDX_Text(pDX, IDC_BEFOREBRIGHT_STATIC, m_BrightStaticVal);
	DDX_Control(pDX, IDC_CHECK1, m_BrightCheck1);
	DDX_Control(pDX, IDC_CHECK2, m_BrightCheck2);
	DDX_Control(pDX, IDC_CHECK3, m_BrightCheck3);
	DDX_Control(pDX, IDC_RICHEDIT22, m_BrightRich);
	DDX_Control(pDX, IDC_STATIC101, m_Working);
}


BEGIN_MESSAGE_MAP(CBeforeBrightDlg, CDialogEx)
	ON_BN_CLICKED(IDC_EXCELTOSQL, &CBeforeBrightDlg::OnBnClickedExceltosql)
	ON_WM_SIZE()
END_MESSAGE_MAP()


// CBeforeBrightDlg 消息处理程序


BOOL CBeforeBrightDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// TODO:  在此添加额外的初始化

	/*list*/
	m_BeforeBright.SetExtendedStyle(m_BeforeBright.GetExtendedStyle() | LVS_EX_FULLROWSELECT | LVS_EX_GRIDLINES);
	m_BeforeBright.InsertColumn(0, _T("机身码"), LVCFMT_CENTER, 150, 0);
	m_BeforeBright.InsertColumn(1, _T("照度值"), LVCFMT_CENTER, 150, 1);
	m_BeforeBright.InsertColumn(2, _T("有线MAC"), LVCFMT_CENTER, 150, 2);
	m_BeforeBright.InsertColumn(3, _T("无线MAC"), LVCFMT_CENTER, 150, 3);
	m_BeforeBright.InsertColumn(4, _T("更新时间"), LVCFMT_CENTER, 150, 4);
	m_BeforeBright.InsertColumn(5, _T("订单号"), LVCFMT_CENTER, 150, 5);
	//map <CString, int> ExcelZiDuan = { { "机身码", 0 }, { "照度值", 1 }, { "无线MAC", 2 }, { "有线MAC", 3 }, { "更新时间", 4 } };
	//ExcelZiDuan.at(pair<CString, int>("机身码", 0));
	//ExcelZiDuan.insert(pair<CString, int>("照度值", 1));
	//ExcelZiDuan.insert(pair<CString, int>("无线MAC", 2));
	//ExcelZiDuan.insert(pair<CString, int>("有线MAC", 3));
	//ExcelZiDuan.insert(pair<CString, int>("更新时间", 4));
	ExcelZiDuan["机身码"] = 1;
	ExcelZiDuan["照度值"] = 2;
	ExcelZiDuan["无线MAC"] = 3;
	ExcelZiDuan["有线MAC"] = 4;
	ExcelZiDuan["更新时间"] = 5;
	ExcelZiDuan[""] = 6;
	BeforeBrightDlg = this;
	BeforeBright.Init(m_hWnd);
	return TRUE;  // return TRUE unless you set the focus to a control
	// 异常:  OCX 属性页应返回 FALSE
}

/*Excel数据导入*/
void CBeforeBrightDlg::OnBnClickedExceltosql() 
{
	// TODO:  在此添加控件通知处理程序代码
	HRESULT hr;
	CString str, stry, strm, strd, strh, strM, strs,BrightSelectSql,ListCtrlFalg,CtrlFlag;
	SYSTEMTIME st;
	CString UpdateBrightToSql,ListFirstColStr,MyTimeStr,ListFirstStr,SubEditVal;
	_variant_t AfterOldTestTime;
	int DataCount,ListRowNum;
	int StaticValLength;
	StaticValLength = m_BrightStaticVal.GetLength();
	ListRowNum = 0;
	UpdateData(TRUE);
	if (DanNum=="")
	{
		MessageBox(_T("请先配置前缀类型和订单号！"), _T("提示"));
		return;
	}
	hr = CoInitialize(NULL);
	if (FAILED(hr))
	{
		AfxMessageBox(_T("Failed to call Coinitialize()"));
	}
	TCHAR szFilter[] = _T("文本文件 | *.xls; *.xlsx");
	CFileDialog fileDlg(TRUE, _T("txt"), NULL, 0, szFilter, this);
	CString strFilePath;
	if (IDOK == fileDlg.DoModal())
	{
		strFilePath = fileDlg.GetPathName();
		//SetDlgItemText(IDC_EDIT1, strFilePath);
	}
	else
	{
		return;
	}
	CApplication app;
	CWorkbook book;
	CWorkbooks books;
	CWorksheet sheet;
	CWorksheets sheets;
	CRange range;
	LPDISPATCH lpDisp;
	//定义变量
	COleVariant covOptional((long)
		DISP_E_PARAMNOTFOUND, VT_ERROR);
	if (!app.CreateDispatch(_T("Excel.Application"), NULL))
	{
		this->MessageBox(_T("无法创建Excel应用"),_T("提示"));
		return;
	}
	books = app.get_Workbooks();
	//打开Excel，其中pathname为Excel表的路径名
	lpDisp = books.Open(strFilePath, covOptional
		, covOptional, covOptional, covOptional, covOptional, covOptional, covOptional
		, covOptional, covOptional, covOptional
		, covOptional, covOptional, covOptional
		, covOptional);
	book.AttachDispatch(lpDisp);
	sheets = book.get_Worksheets();
	sheet = sheets.get_Item(COleVariant((short)1));
	COleVariant vResult,ExcelFirstRowName;
	//读取已经使用区域的信息，包括已经使用的行数、列数、起始行、起始列
	range.AttachDispatch(sheet.get_UsedRange());
	range.AttachDispatch(range.get_Rows());
	//取得已经使用的行数
	long iRowNum = range.get_Count();
	range.AttachDispatch(range.get_Columns());
	//取得已经使用的列数
	long iColNum = range.get_Count();
	//取得已使用区域的起始行，从1开始
	long iStartRow = range.get_Row();
	//取得已使用区域的起始列，从1开始
	long iStartCol = range.get_Column();
	m_BeforeBright.DeleteAllItems();
	m_BrightRich.SetSel(0, -1);
	m_BrightRich.Clear();
	for (int i = iStartRow+1; i <= iRowNum; i++)
	{
		
		MyTimeStr = GetTime();
		CString strRowName = _T("");
		CString ExcleFirstRowNameStr;
		/*strRowName.Format(_T("%d"), i);*/
		/*m_BeforeBright.InsertItem(i - 2, strRowName);*/
		for (int j = iStartCol; j <= iColNum; j++)
		{
			//读取单元格的值

			range.AttachDispatch(sheet.get_Cells());
			range.AttachDispatch(range.get_Item(COleVariant((long)i),
				COleVariant((long)j)).pdispVal);
			vResult = range.get_Value2();
			range.AttachDispatch(sheet.get_Cells());
			range.AttachDispatch(range.get_Item(COleVariant((long)iStartRow),
				COleVariant((long)j)).pdispVal);
			ExcelFirstRowName = range.get_Value2();
			if (ExcelFirstRowName.vt != VT_BSTR)
			{
				m_BeforeBright.DeleteAllItems();
				m_BrightRich.SetSel(0, -1);
				m_BrightRich.Clear();
				MessageBox(_T("请输入正确的Excel表头标题"), _T("提示"));
				/*释放资源*/
				OperateDB.CloseRecordset();
				range.ReleaseDispatch();
				sheet.ReleaseDispatch();
				sheets.ReleaseDispatch();
				book.ReleaseDispatch();
				books.ReleaseDispatch();
				app.Quit();
				app.ReleaseDispatch();
				return;
			}
			ExcleFirstRowNameStr = ExcelFirstRowName.bstrVal;
			if (ExcleFirstRowNameStr == "机身码" || ExcleFirstRowNameStr == "照度值" || ExcleFirstRowNameStr == "无线MAC" || ExcleFirstRowNameStr == "有线MAC" || ExcleFirstRowNameStr == "更新时间")
			{
				if (vResult.vt == VT_BSTR)     //若是字符串
				{
					str = vResult.bstrVal;
				}
				if (vResult.vt == VT_R8) //8字节的数字
				{
					if (ExcleFirstRowNameStr == "机身码")
					{
						str.Format(_T("%g"), vResult.dblVal);
					}
					else
					{
						str.Format(_T("%f"), vResult.dblVal);
					}
				}
				if (ExcleFirstRowNameStr == _T("更新时间")) //时间格式
				{
					COleDateTime oleTime = (COleDateTime)vResult;
					VariantTimeToSystemTime(oleTime, &st);
					stry.Format(_T("%d"), st.wYear);
					strm.Format(_T("%d"), st.wMonth);
					strd.Format(_T("%d"), st.wDay);
					strh.Format(_T("%d"), st.wHour);
					strM.Format(_T("%d"), st.wMinute);
					strs.Format(_T("%d"), st.wSecond);
					str = stry + _T("-") + strm + _T("-") + strd + _T(" ") + strh + _T(":") + strM + _T(":") + strs;
				}
				if (vResult.vt == VT_EMPTY) //单元为空
				{
					str = _T("");
				}
				if (vResult.vt == VT_I4)
				{
					str.Format(_T("%ld"), (int)vResult.lVal);
				}
			}
			else
			{
				m_BeforeBright.DeleteAllItems();
				m_BrightRich.SetSel(0, -1);
				m_BrightRich.Clear();
				MessageBox(_T("请输入正确的Excel表头标题"), _T("提示"));
				/*释放资源*/
				OperateDB.CloseRecordset();
				range.ReleaseDispatch();
				sheet.ReleaseDispatch();
				sheets.ReleaseDispatch();
				book.ReleaseDispatch();
				books.ReleaseDispatch();
				app.Quit();
				app.ReleaseDispatch();
				return;
			}
			try
			{
			switch (ExcelZiDuan[ExcleFirstRowNameStr])
			{
			case 1:
				SubEditVal = str.Left(StaticValLength);
				if (SubEditVal != m_BrightStaticVal)
				{
					m_BrightRich.SetSel(-1, -1);
					m_BrightRich.ReplaceSel(_T("错误的机身码：") + str + _T("\r\n"));
					WritetoTxt(_T("错误的机身码：") + str + _T("\r\n"));
					break;
				}
				BrightSelectSql.Format(_T("SELECT * FROM ProjectorInformation_MainTable WHERE FuselageCode = '%s' and ZhiDan = '%s'"), str,DanNum);
				OperateDB.OpenRecordset(BrightSelectSql);
				DataCount = OperateDB.GetRecordCount();
				if (DataCount == 0)
				{
					m_BrightRich.SetSel(-1, -1);
					m_BrightRich.ReplaceSel(_T("本订单内不存在该机身码：") + str + _T("\r\n"));
					WritetoTxt(_T("本订单内不存在该机身码：") + str + _T("\r\n"));
					/*m_BeforeBright.InsertItem(i - 2, strRowName);*/
					break;
				}
				else
				{
					if (!OperateDB.m_pRecordset->BOF)
					{
						OperateDB.m_pRecordset->MoveFirst();
						AfterOldTestTime = OperateDB.m_pRecordset->GetCollect(_T("PostAgingTestTime"));
						if (AfterOldTestTime.vt == VT_NULL)
						{
							m_BrightRich.SetSel(-1, -1);
							m_BrightRich.ReplaceSel(_T("该产品没有进行老化后测试：") + str + _T("\r\n"));
							WritetoTxt(_T("该产品没有进行老化后测试：") + str + _T("\r\n"));
							/*m_BeforeBright.InsertItem(i - 2, strRowName);*/
							break;
						}
					}
				}
				m_BeforeBright.InsertItem(ListRowNum, str);
				break;
			case 2:
				m_BeforeBright.SetItemText(ListRowNum, 1, str);//i-2
				ListFirstColStr = m_BeforeBright.GetItemText(ListRowNum, 0);
				UpdateBrightToSql.Format(_T("UPDATE ProjectorInformation_MainTable SET IlluminationValue = '%s',LuminanceTestQTime = '%s' WHERE FuselageCode = '%s'"), str, MyTimeStr, ListFirstColStr);
				OperateDB.ExecuteByConnection(UpdateBrightToSql);
				break;
			case 3:
				m_BeforeBright.SetItemText(ListRowNum, 3, str);
				ListFirstColStr = m_BeforeBright.GetItemText(ListRowNum, 0);
				UpdateBrightToSql.Format(_T("UPDATE ProjectorInformation_MainTable SET wirelessMAC = '%s', LuminanceTestQTime = '%s'WHERE FuselageCode = '%s'"), str, MyTimeStr, ListFirstColStr);
				OperateDB.ExecuteByConnection(UpdateBrightToSql);
				break;
			case 4:
				m_BeforeBright.SetItemText(ListRowNum, 2, str);
				ListFirstColStr = m_BeforeBright.GetItemText(ListRowNum, 0);
				UpdateBrightToSql.Format(_T("UPDATE ProjectorInformation_MainTable SET WiredMAC = '%s', LuminanceTestQTime = '%s' WHERE FuselageCode = '%s'"), str, MyTimeStr, ListFirstColStr);
				OperateDB.ExecuteByConnection(UpdateBrightToSql);
				break;
			case 5:
				m_BeforeBright.SetItemText(ListRowNum, 4, str);
				ListFirstColStr = m_BeforeBright.GetItemText(ListRowNum, 0);
				UpdateBrightToSql.Format(_T("UPDATE ProjectorInformation_MainTable SET LuminanceTestTime = '%s', LuminanceTestQTime = '%s' WHERE FuselageCode = '%s'"), str, MyTimeStr, ListFirstColStr);
				OperateDB.ExecuteByConnection(UpdateBrightToSql);
				break;
			case 6:
				break;
			default:
				break;
			}
		}
		   catch (_com_error &e)
		   {
			   OperateDB.m_szErrorMsg = e.ErrorMessage();
			   return;
		   }
		   if (DataCount == 0 || AfterOldTestTime.vt == VT_NULL)
		   {
			   ListRowNum = ListRowNum;
               break;
		   }
		   
		}
		ListFirstStr = m_BeforeBright.GetItemText(ListRowNum, 0);
		if (ListFirstStr!="")
		{
			m_BeforeBright.SetItemText(ListRowNum, 5, DanNum);
			ListRowNum++;
		}
		/*else
		{
			m_BeforeBright.DeleteItem(i - 2);
		}*/	
		SetDlgItemText(IDC_STATIC101,_T("导入中..."));
	}
	/*CString NullFlag;
	ListCount = m_BeforeBright.GetItemCount();
	for (ListStraRow = m_BeforeBright.GetItemCount() - 1; ListStraRow >= 0; ListStraRow--)
	{
		NullFlag = m_BeforeBright.GetItemText(ListStraRow, 0);
		if (NullFlag == "")
		{
			m_BeforeBright.DeleteItem(ListStraRow);
		}
	}*/
	ListCtrlFalg = m_BeforeBright.GetItemText(0,0);
	if (ListCtrlFalg == "")
	{
		MessageBox(_T("未导入任何数据或者全部导入失败，请确认是否导入了错误的表格"), _T("提示"));
		m_BeforeBright.DeleteAllItems();
		BrightFirstRow = 0;
		SetDlgItemText(IDC_STATIC101, _T(""));
		ListRowNum = 0;
	}
	else
	{
		MessageBox(_T("导入成功"), _T("提示"));
		SetDlgItemText(IDC_STATIC101, _T(""));
	}	
	/*释放资源*/
	OperateDB.CloseRecordset();
	range.ReleaseDispatch();
	sheet.ReleaseDispatch();
	sheets.ReleaseDispatch();
	book.ReleaseDispatch();
	books.ReleaseDispatch();
	app.Quit();
	app.ReleaseDispatch();
}

/*获取当前时间*/
CString CBeforeBrightDlg::GetTime()
{
	CTime time = CTime::GetCurrentTime();
	CString Tiemstr;
	Tiemstr = time.Format(_T("%Y-%m-%d  %H:%M:%S"));
	return Tiemstr;
}


/*回车响应*/
BOOL CBeforeBrightDlg::PreTranslateMessage(MSG* pMsg)
{
	// TODO:  在此添加专用代码和/或调用基类
	CString BrightTimeStr, m_BrightBodyNumValStr, InsertBrighttoSql,UpdateBrightToSql;
	CString mOpticalCode, SelectSqlEdit2, SelectSqlEdit1, mMainBoardCode;
	LONG BrightRecodestCount = 0;
	int m_BrightBodyNumStaticValLength;
	_variant_t AfterOldTime;
	BrightTimeStr = GetTime();
	UpdateData(TRUE);
	if (pMsg->message == WM_KEYDOWN && pMsg->wParam == VK_RETURN)
	{
		if (GetFocus()->GetDlgCtrlID() == IDC_BRIGHT_BODY)
		{
			try
			{	
				if (DanNum=="")
				{
					MessageBox(_T("请先配置前缀和订单号！"), _T("提示"));
					m_BrightBodyEditVal = _T("");
					UpdateData(FALSE);
					m_BrightBodyEdit.SetFocus();
					return CDialogEx::PreTranslateMessage(pMsg);
				}
				m_BrightBodyNumStaticValLength = m_BrightStaticVal.GetLength();
				m_BrightBodyNumValStr = m_BrightBodyEditVal.Left(m_BrightBodyNumStaticValLength);
				if (m_BrightBodyNumValStr != m_BrightStaticVal || m_BrightBodyEditVal == "")
				{
					MessageBox(_T("机身码错误！"), _T("提示"));
					m_BrightBodyEditVal = _T("");
					UpdateData(FALSE);
					m_BrightBodyEdit.SetFocus();
					return CDialogEx::PreTranslateMessage(pMsg);
				}
				SelectSqlEdit1.Format(_T("SELECT * FROM ProjectorInformation_MainTable WHERE FuselageCode = '%s' and ZhiDan = '%s'"), m_BrightBodyEditVal,DanNum);
				OperateDB.OpenRecordset(SelectSqlEdit1);
				BrightRecodestCount = OperateDB.GetRecordCount();
				if (BrightRecodestCount == 0)
				{
					MessageBox(_T("本订单内不存在该机身码"), _T("提示"));
					m_BrightBodyEditVal = _T("");
					UpdateData(FALSE);
					m_BrightBodyEdit.SetFocus();
					OperateDB.CloseRecordset();
					return CDialogEx::PreTranslateMessage(pMsg);
				}
				if (!OperateDB.m_pRecordset->BOF)
					OperateDB.m_pRecordset->MoveFirst();
				AfterOldTime = OperateDB.m_pRecordset->GetCollect(_T("PostAgingTestTime"));
				if (AfterOldTime.vt==VT_NULL)
				{
					MessageBox(_T("该产品没有做老化后测试"), _T("提示"));
					m_BrightBodyEdit.SetFocus();
					m_BrightBodyEditVal = "";
					UpdateData(FALSE);
					OperateDB.CloseRecordset();
					return CDialogEx::PreTranslateMessage(pMsg);
				}
				OperateDB.CloseRecordset();
			}
			catch (_com_error &e)
			{
				OperateDB.m_szErrorMsg = e.ErrorMessage();
				return CDialogEx::PreTranslateMessage(pMsg);
			}			
			if (m_WiredLessEditState == TRUE&&m_WiredEditState == FALSE)
			{
				m_WiredLessEdit.EnableWindow(TRUE);
				m_WiredLessEdit.SetFocus();
				pMsg->message = 0;
				pMsg->wParam = 0;
			}
			if (m_WiredEditState == TRUE&&m_WiredLessEditState == FALSE)
			{
				m_WiredEdit.EnableWindow(TRUE);
				m_WiredEdit.SetFocus();
				pMsg->message = 0;
				pMsg->wParam = 0;
			}
			if (m_WiredEditState == TRUE&&m_WiredLessEditState == TRUE)
			{
				m_WiredLessEdit.EnableWindow(TRUE);
				m_WiredLessEdit.SetFocus();
				pMsg->message = 0;
				pMsg->wParam = 0;
			}
		}
		if (GetFocus()->GetDlgCtrlID() == IDC_WIREDLESSMAC&&pMsg->message == WM_KEYDOWN && pMsg->wParam == VK_RETURN)
		{
			if (m_WiredEditState == FALSE)
			{
				try
				{
					UpdateBrightToSql.Format(_T("UPDATE ProjectorInformation_MainTable SET wirelessMAC='%s',LuminanceTestQTime = '%s' WHERE FuselageCode = '%s'"), m_WiredLessEditVal, BrightTimeStr, m_BrightBodyEditVal);
					OperateDB.ExecuteByConnection(UpdateBrightToSql);
					m_BeforeBright.InsertItem(BrightFirstRow, m_BrightBodyEditVal);
					m_BeforeBright.SetItemText(BrightFirstRow, 3, m_WiredLessEditVal);
					m_BeforeBright.SetItemText(BrightFirstRow, 4, BrightTimeStr);
					m_BeforeBright.SetItemText(BrightFirstRow, 5, DanNum);
					BrightFirstRow++;
					m_WiredLessEditVal = _T("");
					m_BrightBodyEditVal = _T("");
					UpdateData(FALSE);
					m_BrightBodyEdit.SetFocus();
					m_WiredLessEdit.EnableWindow(FALSE);
				}
				catch (_com_error &e)
				{
					OperateDB.m_szErrorMsg = e.ErrorMessage();
					return CDialogEx::PreTranslateMessage(pMsg);
				}
			}
			else
			{
				m_WiredEdit.EnableWindow(TRUE);
				m_WiredEdit.SetFocus();

				pMsg->message = 0;
				pMsg->wParam = 0;
			}
		}
		if (GetFocus()->GetDlgCtrlID() == IDC_WIREDMAC&&pMsg->message == WM_KEYDOWN && pMsg->wParam == VK_RETURN)
		{			
			try
			{
				UpdateBrightToSql.Format(_T("UPDATE ProjectorInformation_MainTable SET wirelessMAC='%s',WiredMAC = '%s',LuminanceTestQTime = '%s' WHERE FuselageCode = '%s'"), m_WiredLessEditVal, m_WiredEditVal, BrightTimeStr, m_BrightBodyEditVal);
				OperateDB.ExecuteByConnection(UpdateBrightToSql);
				m_BeforeBright.InsertItem(BrightFirstRow, m_BrightBodyEditVal);
				m_BeforeBright.SetItemText(BrightFirstRow, 3, m_WiredLessEditVal);
				m_BeforeBright.SetItemText(BrightFirstRow, 4, BrightTimeStr);
				m_BeforeBright.SetItemText(BrightFirstRow, 2, m_WiredEditVal);
				m_BeforeBright.SetItemText(BrightFirstRow, 5, DanNum);
				BrightFirstRow++;
				m_WiredLessEditVal = _T("");
				m_BrightBodyEditVal = _T("");
				m_WiredEditVal = _T("");
				UpdateData(FALSE);
				m_BrightBodyEdit.SetFocus();
				m_WiredLessEdit.EnableWindow(FALSE);
				m_WiredEdit.EnableWindow(FALSE);
			}
			catch (_com_error &e)
			{
				OperateDB.m_szErrorMsg = e.ErrorMessage();
				return CDialogEx::PreTranslateMessage(pMsg);
			}
		}
		//if (pMsg->message != 0 && pMsg->wParam != 0)
		//{
		//	BrightFirstRow++;
		//}
	}

	return CDialogEx::PreTranslateMessage(pMsg);
}


void CBeforeBrightDlg::OnOK()
{
	// TODO:  在此添加专用代码和/或调用基类

	//CDialogEx::OnOK();
}


void CBeforeBrightDlg::OnCancel()
{
	// TODO:  在此添加专用代码和/或调用基类

	//CDialogEx::OnCancel();
}


void CBeforeBrightDlg::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);

	// TODO:  在此处添加消息处理程序代码
	if (nType == SIZE_RESTORED || nType == SIZE_MAXIMIZED)
	{
		BeforeBright.ResizeWindow();
	}
}

/*查null*/
CString CBeforeBrightDlg::CheckNull(_variant_t Source)
{
	CString DestStr;
	if (Source.vt == VT_NULL)
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

/*删除List空行*/
void CBeforeBrightDlg::DeleteNullList(CListCtrl Contrl)
{
	int StraRow,ListCount;
	CString NullFlag;
	ListCount = Contrl.GetItemCount();
	for (StraRow = 0; StraRow < ListCount; StraRow++)
	{
		NullFlag = Contrl.GetItemText(StraRow,0);
		if (NullFlag=="")
		{
			Contrl.DeleteItem(StraRow);
		}
	}
}

/*获取日期*/
CString  CBeforeBrightDlg::GetDate()
{
	CTime time = CTime::GetCurrentTime();
	CString DateStr,DayStr,MonthStr,YearStr;
	int day, year, month;
	day = time.GetDay();
	month = time.GetMonth();
	year = time.GetYear();
	DayStr.Format(_T("%d"), day);
	MonthStr.Format(_T("%d"), month);
	YearStr.Format(_T("%d"),year);
	DateStr = "-Default："+YearStr + "-" + MonthStr + "-" + DayStr;
	return DateStr;
}

/*获取exe路径*/
CString CBeforeBrightDlg::GetExePath()
{
	char sFileName[256] = { 0 };
	CString ProjectPath = _T("");
	GetModuleFileName(AfxGetInstanceHandle(), sFileName, 255);
	ProjectPath.Format("%s", sFileName);
	int pos = ProjectPath.ReverseFind('\\');
	if (pos != -1)
		ProjectPath = ProjectPath.Left(pos);
	else
		ProjectPath = _T("");
	return ProjectPath;
}

/*写数据到txt文件*/
BOOL CBeforeBrightDlg::WritetoTxt(CString sValue)
{
	CString SubPath;
	SubPath = GetDate();
	SubPath = DanNum  + SubPath;
	CString sFile = GetExePath() + "\\" + SubPath + ".txt";
	CStdioFile file;
	if (file.Open(sFile, CFile::modeCreate | CFile::modeWrite | CFile::modeNoTruncate))
	{
		file.SeekToEnd(); // 移动文件指针到末尾
		file.WriteString(sValue);
		file.Close();
	}
	return FALSE;
}