
// ProjectorTestSystemDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "ProjectorTestSystem.h"
#include "ProjectorTestSystemDlg.h"
#include "afxdialogex.h"
#include<afxcmn.h>
#include "AdoDB.h"
#include "AdminiPassDlg.h"
#include "SetIndexDlg.h"
#include "ResizeCtrl.h"

/*状态栏*/
static UINT BASED_CODE indicators[] =
{
	IDS_STATEBAR
};

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

/*全局变量*/
BOOL ConnectFlag = FALSE;
//BOOL GetOnFlag = FALSE;
CProjectorTestSystemDlg * ProjectorTestSystemDlg;
extern CMainDlg *MainDlg;
extern CPloDlg *PloDlg;
extern CBefroeOldDlg *BrforeOldDlg;
extern COldUpDlg *OldUpDlg;
extern COldDownDlg *OldDownDlg;
extern CAfterOldDlg *AfterOldDlg;
extern CBeforeBrightDlg *BeforeBrightDlg;
extern CFixDlg *FixDlg;
extern CPackDlg *PackDlg;
CWindowSizeMange PJTestSystemDlg;
// 用于应用程序“关于”菜单项的 CAboutDlg 对话框MO

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// 对话框数据
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

// 实现
protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnAdminiGetOn();
};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
	//ON_COMMAND(ID_32771, &CAboutDlg::OnAdminiGetOn)
END_MESSAGE_MAP()


// CProjectorTestSystemDlg 对话框



CProjectorTestSystemDlg::CProjectorTestSystemDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CProjectorTestSystemDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDI_MYICON);
}

void CProjectorTestSystemDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_TAB1, m_Tab);
}

BEGIN_MESSAGE_MAP(CProjectorTestSystemDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_NOTIFY(TCN_SELCHANGE, IDC_TAB1, &CProjectorTestSystemDlg::OnTcnSelchangeTab1)
	ON_COMMAND(ID_32771, &CProjectorTestSystemDlg::OnAdminiGetOn)
	ON_COMMAND(ID_32772, &CProjectorTestSystemDlg::OnSetIndex)
	ON_COMMAND(ID_32773, &CProjectorTestSystemDlg::OnLogOut)
	ON_COMMAND(ID_32775, &CProjectorTestSystemDlg::OnExitSystem)
	ON_WM_SIZE()
END_MESSAGE_MAP()


// CProjectorTestSystemDlg 消息处理程序

BOOL CProjectorTestSystemDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();
	// 将“关于...”菜单项添加到系统菜单中。

	// IDM_ABOUTBOX 必须在系统命令范围内。
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// 设置此对话框的图标。  当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO:  在此添加额外的初始化代码

	/*获取控件原始位置*/
	PJTestSystemDlg.Init(m_hWnd);

	/*获取窗口大小*/
	CRect DlgRect;
	GetClientRect(&DlgRect);     //取客户区大小    
	old.x = DlgRect.right - DlgRect.left;
	old.y = DlgRect.bottom - DlgRect.top;

	/*状态栏*/
	m_StateBar.Create(this);//创建状态栏
	m_StateBar.SetIndicators(indicators, sizeof(indicators) / sizeof(UINT)); //设置状态栏数目
	CRect StataBarRect;
	GetClientRect(&StataBarRect);
	m_StateBar.SetPaneInfo(0, IDS_STATEBAR, SBPS_NORMAL, StataBarRect.Width());
	RepositionBars(AFX_IDW_CONTROLBAR_FIRST, AFX_IDW_CONTROLBAR_LAST, IDS_STATEBAR);

	/*标签页*/
	CRect tabRect;   // 标签控件客户区的位置和大小   
	m_Tab.InsertItem(0, _T("查询主界面"),0);  
	m_Tab.InsertItem(1, _T("打光机、主板"),1);
	m_Tab.InsertItem(2, _T("老化前测试位"),2);  
	m_Tab.InsertItem(3, _T("老化（上架时间）"),3);
	m_Tab.InsertItem(4, _T("老化（下架时间）"),4);
	m_Tab.InsertItem(5, _T("老化后测试"),5);
	m_Tab.InsertItem(6, _T("亮度测试前"),6);
	m_Tab.InsertItem(7, _T("维  修"),7);
	m_Tab.InsertItem(8, _T("包  装"),8);	
	m_Main.Create(IDD_MAIN, &m_Tab);    // 创建第一个标签页   
	m_Plo.Create(IDD_PLO, &m_Tab); // 创建第二个标签页
	m_BeforeOld.Create(IDD_BEFOREOLD, &m_Tab);
	m_OldUp.Create(IDD_OLDUP, &m_Tab);
	m_OldDown.Create(IDD_OLDDOWN, &m_Tab);
	m_AfterOld.Create(IDD_AFTEROLD, &m_Tab);
	m_BeforeBright.Create(IDD_BEFOREBRIGHT, &m_Tab);
	m_Fix.Create(IDD_FIX, &m_Tab);
	m_Pack.Create(IDD_PACK, &m_Tab);
	m_Tab.GetClientRect(&tabRect);    // 获取标签控件客户区Rect   
	// 调整tabRect，使其覆盖范围适合放置标签页   
	/*tabRect.left += 1;
	tabRect.right -= 1;	
	tabRect.bottom -= 1;*/
	tabRect.top += 25;
	// 根据调整好的tabRect放置m_jzmDlg子对话框，并设置为显示   
	m_Main.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
	m_Plo.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_SHOWWINDOW);
	m_BeforeOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
	m_OldUp.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
	m_OldDown.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
	m_AfterOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
	m_BeforeBright.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
	m_Fix.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
	m_Pack.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
	m_Tab.SetCurSel(1);

	ProjectorTestSystemDlg = this;

	return FALSE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CProjectorTestSystemDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。  对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CProjectorTestSystemDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作区矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CProjectorTestSystemDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

/*标签页响应*/
void CProjectorTestSystemDlg::OnTcnSelchangeTab1(NMHDR *pNMHDR, LRESULT *pResult)
{
	// TODO:  在此添加控件通知处理程序代码
	*pResult = 0;
	CRect tabRect;    // 标签控件客户区的Rect   
	// 获取标签控件客户区Rect，并对其调整，以适合放置标签页   
	m_Tab.GetClientRect(&tabRect);
	tabRect.left += 1;
	tabRect.right -= 1;
	tabRect.top += 25;
	tabRect.bottom -= 1;
	switch (m_Tab.GetCurSel())
	{ 
	case 0:
		if (GetOnFlag == TRUE)
		{
			m_Main.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_SHOWWINDOW);
			m_Plo.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldUp.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldDown.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_AfterOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeBright.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Fix.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Pack.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Main.m_MainZhiDan.SetFocus();
		}
		else
		{
			m_Tab.SetCurSel(1);
			MessageBox(_T("此操作需要管理员权限，请先登录"));
		}
		break;
	case 1:
		if (GetOnFlag == TRUE)
		{
			m_Main.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Plo.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_SHOWWINDOW);
			m_BeforeOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldUp.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldDown.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_AfterOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeBright.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Fix.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Pack.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Plo.m_PloBodyNumSub.SetFocus();
		}
		else
		{
			m_Tab.SetCurSel(1);
			MessageBox(_T("此操作需要管理员权限，请先登录"));
		}
		break;
	case 2:
		if (GetOnFlag == TRUE)
		{
			m_Main.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Plo.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_SHOWWINDOW);
			m_OldUp.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldDown.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_AfterOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeBright.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Fix.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Pack.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeOld.m_BeforeOldEditContrl.SetFocus();
		}
		else
		{
			m_Tab.SetCurSel(1);
			MessageBox(_T("此操作需要管理员权限，请先登录"));
		}
		break;
	case 3:
		if (GetOnFlag == TRUE)
		{
			m_Main.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Plo.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldUp.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_SHOWWINDOW);
			m_OldDown.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_AfterOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeBright.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Fix.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Pack.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldUp.m_OldUpEdit.SetFocus();
		}
		else
		{
			m_Tab.SetCurSel(1);
			MessageBox(_T("此操作需要管理员权限，请先登录"));
		}
		break;
	case 4:
		if (GetOnFlag == TRUE)
		{
			m_Main.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Plo.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldUp.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldDown.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_SHOWWINDOW);
			m_AfterOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeBright.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Fix.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Pack.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldDown.m_OldDownEdit.SetFocus();
		}
		else
		{
			m_Tab.SetCurSel(1);
			MessageBox(_T("此操作需要管理员权限，请先登录"));
		}
		break;
	case 5:
		if (GetOnFlag == TRUE)
		{
			m_Main.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Plo.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldUp.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldDown.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_AfterOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_SHOWWINDOW);
			m_BeforeBright.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Fix.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Pack.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_AfterOld.m_AfterOldEditContrl.SetFocus();
		}
		else
		{
			m_Tab.SetCurSel(1);
			MessageBox(_T("此操作需要管理员权限，请先登录"));
		}
		break;
	case 6:
		if (GetOnFlag == TRUE)
		{
			m_Main.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Plo.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldUp.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldDown.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_AfterOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeBright.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_SHOWWINDOW);
			m_Fix.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Pack.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeBright.m_BrightBodyEdit.SetFocus();
		}
		else
		{
			m_Tab.SetCurSel(1);
			MessageBox(_T("此操作需要管理员权限，请先登录"));
		}
		break;
	case 7:
		if (GetOnFlag == TRUE)
		{
			m_Main.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Plo.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldUp.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldDown.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_AfterOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeBright.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Fix.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_SHOWWINDOW);
			m_Pack.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Fix.m_FixSingleNumEdit.SetFocus();
		}
		else
		{
			m_Tab.SetCurSel(1);
			MessageBox(_T("此操作需要管理员权限，请先登录"));
		}
		break;
	case 8:
		if (GetOnFlag == TRUE)
		{
			m_Main.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Plo.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldUp.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_OldDown.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_AfterOld.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_BeforeBright.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Fix.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_HIDEWINDOW);
			m_Pack.SetWindowPos(NULL, tabRect.left, tabRect.top, tabRect.Width(), tabRect.Height(), SWP_SHOWWINDOW);
			m_Pack.m_PackEdit.SetFocus();

		}
		else
		{
			m_Tab.SetCurSel(1);
			MessageBox(_T("此操作需要管理员权限，请先登录"));
		}
		break;
	default:
		break;
	}
}

/*读取项目可执行文件路径*/
CString CProjectorTestSystemDlg::GetExePath()
{
	char sFileName[256] = { 0 };
	CString ProjectPath = _T("");
	GetModuleFileName(AfxGetInstanceHandle(), sFileName, 255);
	ProjectPath.Format(_T("%s"), sFileName);
	int pos = ProjectPath.ReverseFind('\\');
	if (pos != -1)
		ProjectPath = ProjectPath.Left(pos);
	else
		ProjectPath = _T("");
	return ProjectPath;
}

/*从ini文件获取数据库连接字符串*/
void CProjectorTestSystemDlg::GetConnectStr()
{
	TCHAR ServerName[200];
	TCHAR UserIDChar[200];
	TCHAR PasswordChar[200];
	TCHAR DataBaseNameChar[200];
	CString IniPath, IniName;
	IniPath = GetExePath();
	IniName = _T("\\DBPATH.ini");
	IniPath += IniName;
	GetPrivateProfileString(_T("DBPATH"), _T("Data Source"), _T(""), ServerName, sizeof(ServerName), IniPath);
	GetPrivateProfileString(_T("DBPATH"), _T("User ID"), _T(""), UserIDChar, sizeof(UserIDChar), IniPath);
	GetPrivateProfileString(_T("DBPATH"), _T("Password"), _T(""), PasswordChar, sizeof(PasswordChar), IniPath);
	GetPrivateProfileString(_T("DBPATH"), _T("Initial Catalog"), _T(""), DataBaseNameChar, sizeof(DataBaseNameChar), IniPath);
	OperateDB.DataBaseName = DataBaseNameChar;
	OperateDB.DataSource = ServerName;
	OperateDB.UserId = UserIDChar;
	OperateDB.Passwoord = PasswordChar;
	OperateDB.m_szConnectionString = _T("Provider = SQLOLEDB.1; Data Source = ") + OperateDB.DataSource + _T("; Network Library = DBMSSOCN; Initial Catalog = ") + OperateDB.DataBaseName;
}

/*菜单--管理员登录*/
void CProjectorTestSystemDlg::OnAdminiGetOn()
{
	// TODO:  在此添加命令处理程序代码
	INT_PTR AdminiGetOnRes;
	CAdminiPassDlg m_AdminiPass;
	AdminiGetOnRes = m_AdminiPass.DoModal();
	if (IDCANCEL == AdminiGetOnRes)
	{
		return;
	}
	if (PassWord==_T("admini"))
	{
		GetConnectStr();
		OperateDB.OpenConnection();
		ConnectFlag = OperateDB.IsConnected();
		if (ConnectFlag==TRUE)
		{
			GetOnFlag = TRUE;
			m_StateBar.SetPaneText(0,_T("已登录"));
			GetMenu()->GetSubMenu(0)->EnableMenuItem(0, MF_BYPOSITION | MF_DISABLED);
			GetMenu()->GetSubMenu(0)->EnableMenuItem(1, MF_BYPOSITION | MF_ENABLED);
			GetMenu()->GetSubMenu(0)->EnableMenuItem(2, MF_BYPOSITION | MF_ENABLED);
		}
		else
		{
			MessageBox(_T("登录失败，请重新登录"));
			GetOnFlag = FALSE;
			return;
		}
	}
	else
	{
		GetOnFlag = FALSE;
		MessageBox(_T("密码错误！"));
		return;
	}
	
}

/*菜单--配置前缀*/
void CProjectorTestSystemDlg::OnSetIndex()
{
	// TODO:  在此添加命令处理程序代码
	INT_PTR SetIndexnRes;
	CString IndexName;	
	SetIndexnRes = SetIndex.DoModal();
	if (SetIndexnRes==IDCANCEL)
	{
		return;
	}
	//PloDlg->SetDlgItemTextA(IDC_ZHIDANNUM, DanNum);
}

/*注销登录*/
void CProjectorTestSystemDlg::OnLogOut()
{
	// TODO:  在此添加命令处理程序代码
	BOOL CloseFinshFlag = FALSE;
	CloseFinshFlag = OperateDB.CloseConnection();
	if (CloseFinshFlag==TRUE)
	{
		GetOnFlag = FALSE;
		ConnectFlag = FALSE;
		m_StateBar.SetPaneText(0, _T("未登录"));
		GetMenu()->GetSubMenu(0)->EnableMenuItem(0, MF_BYPOSITION | MF_ENABLED);
		GetMenu()->GetSubMenu(0)->EnableMenuItem(1, MF_BYPOSITION | MF_DISABLED);
		GetMenu()->GetSubMenu(0)->EnableMenuItem(2, MF_BYPOSITION | MF_DISABLED);
	}
}

/*退出系统*/
void CProjectorTestSystemDlg::OnExitSystem()
{
	// TODO:  在此添加命令处理程序代码
	OperateDB.CloseConnection();
	OnCancel();
}

/*类向导中的Cancel函数*/
void CProjectorTestSystemDlg::OnCancel()
{
	// TODO:  在此添加专用代码和/或调用基类

	CDialogEx::OnCancel();
}

/*获取当前时间*/
CString CProjectorTestSystemDlg::GetTime()
{
	CTime time = CTime::GetCurrentTime();
	CString Tiemstr;
	Tiemstr = time.Format(_T("%Y-%m-%d  %H:%M:%S"));
	return Tiemstr;
}


void CProjectorTestSystemDlg::OnSize(UINT nType, int cx, int cy)
{
	CDialogEx::OnSize(nType, cx, cy);

	// TODO:  在此处添加消息处理程序代码
	if (nType == SIZE_RESTORED || nType == SIZE_MAXIMIZED)
	{
		/*状态栏*/
		if (m_StateBar.GetSafeHwnd())
			{
				//RepositionBars(AFX_IDW_CONTROLBAR_FIRST, AFX_IDW_CONTROLBAR_LAST, 0);  
				CRect    m_Rect, rectBar;
				GetClientRect(&m_Rect);         //获取对话框的矩形区域
				m_StateBar.GetClientRect(&rectBar);
				m_Rect.top = m_Rect.bottom - 20;    //设置状态栏的矩形区域
				m_StateBar.MoveWindow(0, cy - m_Rect.Height(), m_Rect.Width(), rectBar.Height());
			}		
		PJTestSystemDlg.ResizeWindow();
		CRect TabCtrlRect;
		CWnd *TabPtr;
		TabPtr = GetDlgItem(IDC_TAB1);
		if (TabPtr == NULL)
		{
			return;
		}
		TabPtr->GetClientRect(&TabCtrlRect);
		TabCtrlRect.top += 25;
		MainDlg->MoveWindow(&TabCtrlRect, TRUE);		
		PloDlg->MoveWindow(&TabCtrlRect, TRUE);
		BrforeOldDlg->MoveWindow(&TabCtrlRect, TRUE);
		OldUpDlg->MoveWindow(&TabCtrlRect, TRUE);
		OldDownDlg->MoveWindow(&TabCtrlRect, TRUE);
		AfterOldDlg->MoveWindow(&TabCtrlRect, TRUE);
		BeforeBrightDlg->MoveWindow(&TabCtrlRect, TRUE);
		FixDlg->MoveWindow(&TabCtrlRect, TRUE);
		PackDlg->MoveWindow(&TabCtrlRect, TRUE);
	}
}
