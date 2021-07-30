<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td colspan="2">
                    <h1>流水帳管理系統 - 後台</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="UserInfo.aspx">使用者資訊</a> <br />
                    <a href="AccountingList.aspx">流水帳管理</a>
                </td>
                <td>
                    <!-- Main -->
                    <asp:Button ID="btnCreate" runat="server"  OnClick="btnCreate_Click"  Text="Add Accounting" />
                    <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAccountingList_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="標題" DataField="caption" />
                            <asp:TemplateField HeaderText="金額">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblAmount" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IN / OUT">
                                <ItemTemplate>
                                  <!--  <%# ((int)Eval("act_type") == 0) ? "支出" : "收入" %> -->
                                    <asp:Literal runat="server" ID="ltActType" ></asp:Literal>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="建立日期" DataField="create_date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:TemplateField HeaderText="Act">
                                <ItemTemplate>
                                    <a href="/SystemAdmin/AccountingDetail.aspx?list_id=<%# Eval("list_id") %>">Edit</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                        <p style="color: red; background-color: cornflowerblue">
                            No data in your Accounting Note.
                        </p>
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
