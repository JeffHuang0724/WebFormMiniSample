<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote.SystemAdmin.AccountingList" %>

<%@ Register Src="~/UserControls/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="server">
    <h1 style="margin-left: 10rem;">流水帳管理</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHeader2" runat="server">
    <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" Text="Add" Font-Size="Medium"/>
    <asp:Label runat="server" ID="lblAmount" Style="margin-left: 10rem" Visible="true"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentBody" runat="server">
    <div style="margin-top: 0.5rem;">
        <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAccountingList_RowDataBound" CellPadding="10" ForeColor="#333333" GridLines="Both">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="IN / OUT">
                    <ItemTemplate>
                        <!--  <%# ((int)Eval("ActType") == 0) ? "支出" : "收入" %> -->
                        <asp:Literal runat="server" ID="ltActType"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="金額">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblAmount"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="標題" DataField="caption" />
                <asp:TemplateField HeaderText="Act">
                    <ItemTemplate>
                        <a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">Edit</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <div style="margin-top: 1rem;">
            <uc1:ucPager runat="server" ID="ucPager" PageSize="10" CurrentPage="1" TotalSize="10" Url="/SystemAdmin/AccountingList.aspx" />
            <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                <p style="color: red; background-color: cornflowerblue;">
                    No data in your Accounting Note.
                </p>
            </asp:PlaceHolder>
        </div>
    </div>
</asp:Content>
