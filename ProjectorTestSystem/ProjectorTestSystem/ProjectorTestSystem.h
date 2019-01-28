
// ProjectorTestSystem.h : PROJECT_NAME 应用程序的主头文件
//

#pragma once

#ifndef __AFXWIN_H__
	#error "在包含此文件之前包含“stdafx.h”以生成 PCH 文件"
#endif

#include "resource.h"		// 主符号
#include "AdoDB.h"

// CProjectorTestSystemApp: 
// 有关此类的实现，请参阅 ProjectorTestSystem.cpp
//

class CProjectorTestSystemApp : public CWinApp
{
public:
	CProjectorTestSystemApp();

// 重写
public:
	virtual BOOL InitInstance();

// 实现

	DECLARE_MESSAGE_MAP()
};

extern CProjectorTestSystemApp theApp;
extern CString  PassWord;
extern CString  DanNum;
extern HTREEITEM hRoot;
extern CAdo OperateDB;
extern CString  PrefixType;
extern CString  BodyNumPrefix;
extern CString  SingleBodyNumPrefix;
extern CString  MainBoardNumPrefix;
extern BOOL GetOnFlag;