<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AccountingDetail.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
    <h1 style="margin-left: 10rem;">流水帳管理</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
    <table>
        <asp:PlaceHolder ID="plcHasData" runat="server" Visible="true">
            <tr>
                <td>
                    <div style="float: left; margin-top: 1rem; margin-left: 10rem;">Type: </div>
                </td>
                <td>
                    <div style="float: left; margin-top: 1rem; margin-left: 1rem;">
                        <asp:DropDownList ID="ddlActType" runat="server">
                            <asp:ListItem Value="0">支出</asp:ListItem>
                            <asp:ListItem Value="1">收入</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="float: left; margin-top: 1.5rem; margin-left: 10rem;">Amount:</div>
                </td>
                <td>
                    <div style="float: left; margin-top: 1.5rem; margin-left: 1rem;">
                        <asp:TextBox ID="txtAmount" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="float: left; margin-top: 1.5rem; margin-left: 10rem;">Caption:</div>
                </td>
                <td>
                    <div style="float: left; margin-top: 1.5rem; margin-left: 1rem;">
                        <asp:TextBox ID="txtCaption" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="float: left; margin-top: 1.5rem; margin-left: 10rem;">Desc:</div>
                </td>
                <td>
                    <div style="float: left; margin-top: 1.5rem; margin-left: 1rem;">
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="float: left;">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" Style="margin-top: 1.5rem; margin-left: 10rem;" Font-Size="Medium" />
                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" Visible="false" Style="margin-top: 1.5rem; margin-left: 6rem;" Font-Size="Medium" />
                    </div>
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2">
                <asp:Literal runat="server" ID="ltMsg"></asp:Literal>
            </td>
        </tr>
    </table>
</asp:Content>
