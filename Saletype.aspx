<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="Saletype.aspx.cs" Inherits="Saletype" %>
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
 function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    function toUpper(txt) {
        document.getElementById(txt).value = document.getElementById(txt).value.toUpperCase();
        return true;
    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="BigFieldSet">
    <legend class="BigLegend">
   <h1 style="position: relative; right: 0px;">
     Sale Type
    </h1>
    </legend>
     
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>
           
             
             <table id="Table1" align="center" runat="server" cellspacing="5">
                      <tr>
                    <td align="right">
                       <asp:Label ID="lblsaletype" runat="server" Text="Sale Type:" CssClass="Label"></asp:Label>
                    </td>
                
                          <td align="left">

                            <asp:DropDownList ID="ddpaymenttype" runat="server" height="25px"  
                              CssClass="DropDown" AutoPostBack="true" onkeyup="return validate(this.id)">
                             <asp:ListItem Text="Credit Card" Value="1"></asp:ListItem>
                             <asp:ListItem Text="Debit card" Value="2"></asp:ListItem>

                               </asp:DropDownList>

                                 
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                                 
                                 </td>
                                
                       </tr>
                       <tr>
                          <td align="right">
                       <asp:Label ID="Label1" runat="server" Text="Extra Amount:" CssClass="Label"></asp:Label>
                    </td>
                
                          <td align="left">

                              <asp:TextBox ID="txtamount" runat="server" TabIndex="1"  CssClass="TextBox" Width="150px"  onkeypress="return alpha(event)"></asp:TextBox>   
                              <span style="color: Red; font: bold 30px 0 'Segoe Ui';" align="center">*</span>
                                 
                                 </td>
                                
                       
                       
                       </tr>

</table>

 <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
                <asp:Button ID="Button1" runat="server" Text="Save" Width="95px" 
                    OnClick="btnsave_Click" CssClass="Buttons embossed-link" TabIndex="2" 
                    Height="33px"/>
               <asp:Button ID="Button2" runat="server"  Text="Exit" Width="90px" onclick="btnexit_Click" CssClass="Buttons embossed-link" TabIndex="3"/>
            </div>

            </fieldset>
</asp:Content>

