<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Backuprestore.aspx.cs" Inherits="Backuprestore" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
<script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
<link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
<script type="text/javascript">
  function HideLabel() {
        var seconds = 3;
        setTimeout(function () {
            document.getElementById("<%=lblsuccess.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <fieldset>
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Back Up
    </h1>
    </legend>
      
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel" ></asp:Label>
    </h3>
   
                                <table align="center">
                                    <tr>
                                        <td align="center">
                                           <asp:Label ID="lbldatabase" runat="server" Text="Select Database:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>

                                        </td>
                                        <td align="left">
                                            &nbsp;&nbsp;<asp:DropDownList ID="ddlDatabases" runat="server" AutoPostBack="false"
                                                Height="23px" Width="197px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnBackup" runat="server" Text="Backup" CssClass="Buttons embossed-link" OnClick="btnBackup_Click"/>
                                             <asp:Button ID="btnexit" runat="server" Text="Exit" Width="95px"  
                        CssClass="Buttons embossed-link" onclick="btnexit_Click"  />
                                        </td>
                                    </tr>
                                    <tr>
                            <td align="center">
                                <asp:Label ID="lblMessage" ForeColor="Red" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        </table>

                         
                                 </fieldset> 

</asp:Content>

