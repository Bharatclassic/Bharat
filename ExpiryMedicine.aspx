<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="ExpiryMedicine.aspx.cs" Inherits="ExpiryMedicine" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script src="JavaScripts/jquery-1.10.0.min.js" type="text/javascript"></script>
  <script src="JavaScripts/jquery-ui.min1.js" type="text/javascript"></script>
  <link href="Styles/jquery-ui1.css" rel="Stylesheet" type="text/css" />
  <script type="text/javascript">
     
      function alpha(e) {
          var k = e.charCode ? e.charCode : e.keyCode;
          return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 32 || k == 8 || k == 37 || k == 39);   //k=9(keycode for tab)
      }
    
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Expiry Medicine
    </h1>
    </legend>
     
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>
     <div align="right">
       <asp:Label ID="Label2" runat="server" Text="Date:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                   <asp:TextBox ID="txtdate1"  runat="server" AutoPostBack="true" Width="80px"
             ontextchanged="txtdate1_TextChanged"></asp:TextBox>
             <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate1" Format="dd/MM/yyyy" runat="server">
                            </asp:CalendarExtender>
             <asp:MaskedEditExtender TargetControlID="txtdate1" ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MessageValidatorTip="true"
                                                OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError"
                                                MaskType="Date" CultureName="en-CA"> </asp:MaskedEditExtender>
             
            
  

             <asp:Label ID="lblerrordate" runat="server"></asp:Label>
       


    
    </div>

     
          <div style="width: 980px; margin: 0 auto; padding: 0" align="center">
           <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
          

              <div>

 <asp:GridView ID="gvDetails" runat="server" Width="629px" CellPadding="4" BackColor="Yellow" DataKeyNames="Productcode"
             AllowPaging="true"  Font-Bold="true" ForeColor="Black" Font-Names="Times New Roman" Font-Size="Small"
                      GridLines="Both"  PageSize="5">
         <PagerStyle BackColor="Yellow" ForeColor="Black" HorizontalAlign="Center" Font-Size="12px"/>
         <Columns>
         <asp:TemplateField>
<ItemTemplate>
<asp:CheckBox ID="chkSelect" runat="server" />
</ItemTemplate>
</asp:TemplateField>
</Columns>
         </asp:GridView>

</div>

 <asp:Panel ID="pdgene" runat="server" ScrollBars="Auto">
        <asp:GridView ID="grpexpiredetails" align="center" runat="server" Width="829px" CellPadding="1" BackColor="White"
            ForeColor="Black" EmptyDataText="No Records Found">
<AlternatingRowStyle BackColor="#F1F1F1" />

<HeaderStyle CssClass="HeaderStyle" />

<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />

<RowStyle CssClass="RowStyle" BorderColor="White" />
</asp:GridView>
   </asp:Panel> 
           </asp:Panel></div>
              
             <asp:HiddenField ID="hfCount" runat="server" Value = "0" />
            

 <div align="left" class="SubmitButtons" style="position:relative; left: 300px; top: 0px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Delete"  Width="95px" TabIndex="2" CssClass="Buttons embossed-link" onclick="btnDelete_Click"/>
              <asp:Button ID="btnprint" runat="server" Text="Print"  Width="95px" TabIndex="3" CssClass="Buttons embossed-link" onclick="btnPrint_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" CssClass="Buttons embossed-link" TabIndex="4" OnClick="btnExit_Click"/>  
                </div>   
               
 </fieldset>
</asp:Content>

