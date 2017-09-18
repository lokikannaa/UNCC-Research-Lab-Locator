<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DepartmentLabs.aspx.cs" Inherits="DepartmentLabs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 201px;
        }
        .auto-style3 {
            width: 201px;
            height: 26px;
        }
        .auto-style4 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" Runat="Server">
    <div id="scroll">
    <asp:GridView ID="gvDeptLabs" runat="server" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" OnSelectedIndexChanged="gvDeptLabs_SelectedIndexChanged" >
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>
            <asp:CommandField ButtonType="Button" CausesValidation="False" ShowSelectButton="True" />
        </Columns>
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <SortedAscendingCellStyle BackColor="#FAFAE7" />
        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
        <SortedDescendingCellStyle BackColor="#E1DB9C" />
        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
    </asp:GridView>
    </div>
    <br />
    <br />

    <asp:Panel ID="pnlAssignLabs" runat="server" Visible="False">
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">LabID</td>
                <td>
                    <asp:TextBox ID="txtLabID" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Assign to</td>
                <td>
                    <asp:DropDownList ID="ddlAssignto" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Value="99">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Assigned By</td>
                <td>
                    <asp:TextBox ID="txtAssignedBy" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Status</td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtStatus" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:Button ID="btnUnassign" runat="server" Text="Unassign" Visible="False" OnClick="btnUnassign_Click"/>
                </td>
                <td>
                    <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" />
                    &nbsp;<asp:Button ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click" Text="Cancel" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>

</asp:Content>

