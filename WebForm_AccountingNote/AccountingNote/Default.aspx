<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccountingNote.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>毛豆端火鍋組 - 流水帳管理系統</title>
</head>
<body style="width: 100%; height: 100%; margin: 0; padding: 0;">
    <form id="form1" runat="server">
        <div style="width: 100%; height: 100%; display: flex; flex-flow: column; align-items: center; justify-content: center; text-align: center">
            <table>
                <tr>
                    <td>
                        <h1 Style="margin-right: 10rem">流水帳管理系統</h1>
                    </td>
                    <td>
                        <a href="Login.aspx">登入系統</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">這是一個提供使用者註冊會員，並擁有流水帳功能的一個網站</td>
                </tr>
            </table>
        </div>
        <div style="width: 100%; height: 100%; display: flex; flex-flow: column; align-items: center; justify-content: center; text-align: center; margin-top: 2rem">
            <table border="1" rules="rows" frame="void">
                <tr>
                    <td>初次記帳</td>
                    <td>
                        <asp:Label runat="server" ID="lblFirstAccountingTime" Text="" Style="margin-left: 2rem"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>最後記帳</td>
                    <td>
                        <asp:Label runat="server" ID="lblLastAccountingTime" Text="" Style="margin-left: 2rem"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>記帳數量</td>
                    <td>
                        <asp:Label runat="server" ID="lblAccountingCount" Text="" Style="margin-left: 2rem"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>會員數</td>
                    <td>
                        <asp:Label runat="server" ID="lblUserCount" Text="" Style="margin-left: 2rem"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
