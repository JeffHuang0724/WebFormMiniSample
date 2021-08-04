<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="AccountingNote.SystemAdmin.UserDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
    <h1 style="margin-left: 10rem;">會員管理</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <table>
        <tr>
            <td>
                <div style="float: left;">帳號: </div>
            </td>
            <td>
                <div style="float: left; margin-left: 1rem;">
                    <asp:Panel runat="server" ID="pnlAccount">
                        <asp:TextBox runat="server" ID="txtUserAccount" Visible="true"></asp:TextBox>
                        <asp:Label runat="server" ID="lblUserAccount" Visible="true"></asp:Label>
                    </asp:Panel>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="float: left; margin-top: 1.5rem;">姓名:</div>
            </td>
            <td>
                <div style="float: left; margin-left: 1rem; margin-top: 1.5rem;">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="float: left; margin-top: 1.5rem;">Email:</div>
            </td>
            <td>
                <div style="float: left; margin-left: 1rem; margin-top: 1.5rem;">
                    <asp:TextBox ID="txtUserEmail" runat="server" TextMode="Email"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="float: left; margin-top: 1.5rem;">等級:</div>
            </td>
            <td>
                <div style="float: left; margin-left: 1rem; margin-top: 1.5rem;">
                    <asp:Label runat="server" ID="lblUserLevel"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="float: left; margin-top: 1.5rem;">建立時間:</div>
            </td>
            <td>
                <div style="float: left; margin-left: 1rem; margin-top: 1.5rem;">
                    <asp:Label runat="server" ID="lblUserCreateTime"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="float: left;">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Style="margin-top: 1.5rem;" Font-Size="Medium" />
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" Visible="false" Style="margin-top: 1.5rem; margin-left: 3rem;" Font-Size="Medium" />
                </div>
            </td>
            <td>
                <div style="float: Right;">
                    <asp:Button ID="btnChangePwd" runat="server" OnClick="btnChangePwd_Click" Text="前往變更密碼" Style="margin-top: 1rem;" Font-Size="Medium" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Literal runat="server" ID="ltMsg"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
