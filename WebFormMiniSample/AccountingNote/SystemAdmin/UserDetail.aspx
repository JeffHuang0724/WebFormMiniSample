<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>毛豆端火鍋組 - 流水帳管理系統</title>
</head>
<body>
     <form id="form1" runat="server">
        <div style="width: 100%; height: 100%; display: flex; flex-flow: column; align-items: center; justify-content: center; text-align: center">
            <table>
                <tr>
                    <td>
                        <h1>流水帳管理系統   -  後台</h1>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%; height: 100%; display: flex; flex-flow: column; align-items: center; justify-content: center; text-align: center">
            <table width="100%" border="1">
                <tr>
                    <td width="15%">
                        <div style="float: left; margin-left: 3rem;">
                            <a href="UserInfo.aspx">使用者資訊</a>
                            <br />
                            <br />
                            <a href="AccountingList.aspx">流水帳管理</a>
                            <br />
                            <br />
                            <a href="/SystemAdmin/UserList.aspx">會員管理 </a>
                        </div>
                    </td>
                    <td width="85%">
                        <div style="text-align: left; margin-left: 15rem;">
                            <h1>會員管理</h1>
                        </div>
                        <div style="float: left; margin-top: 1.5rem; margin-left: 15rem;">
                            <table>
                                <tr>
                                    <td>
                                        <div style="float: left; ">帳號: </div>
                                    </td>
                                    <td>
                                        <div style="float: left; margin-left: 1rem;">
                                            <asp:Label runat="server" ID="lblUserAccount"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="float: left; margin-top:1rem">姓名:</div>
                                    </td>
                                    <td>
                                        <div style="float: left; margin-left: 1rem;margin-top:1rem;">
                                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="float: left;margin-top:1rem;">Email:</div>
                                    </td>
                                    <td>
                                        <div style="float: left; margin-left: 1rem;margin-top:1rem;">
                                            <asp:TextBox ID="txtUserEmail" runat="server" TextMode="Email"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="float: left;margin-top:1rem;">等級:</div>
                                    </td>
                                    <td>
                                        <div style="float: left; margin-left: 1rem;margin-top:1rem;">
                                            <asp:DropDownList ID="ddlUserLevel" runat="server">
                                                <asp:ListItem Value="0">管理員</asp:ListItem>
                                                <asp:ListItem Value="1">一般會員</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="float: left;margin-top:1rem;">建立時間:</div>
                                    </td>
                                    <td>
                                        <div style="float: left; margin-left: 1rem;margin-top:1rem;">
                                            <asp:Label runat="server" ID="lblUserCreateTime"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="float: left; ">
                                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Style="margin-top: 1rem; margin-right: 2.5rem" />
                                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" Visible="false" Style="margin-top: 1rem;" />
                                        </div>
                                    </td>
                                    <td>
                                        <div style="float: Right; ">
                                            <asp:Button ID="btnChangePwd" runat="server" OnClick="btnChangePwd_Click" Text="前往變更密碼" Style="margin-top: 1rem;" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Literal runat="server" ID="ltMsg"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
