<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="SalesReturn.aspx.cs" Inherits="SalesReturn" %>
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
<fieldset class="BigFieldSet">
 <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Sales Return
    </h1>
    </legend>
     
    <h3>
     <asp:Label ID="lblerror" runat="server" Text="" CssClass="ErrorLabel"></asp:Label>
                <asp:Label ID="lblsuccess" runat="server" Text="" CssClass="SuccessLabel"></asp:Label>
    </h3>

 <div align="right">
       <asp:Label ID="Label2" runat="server" Text="Date:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
                   <asp:TextBox ID="txtdate1"  runat="server"></asp:TextBox>
                  <asp:ImageButton ID="ImageButton1" CssClass="Calender" runat="server" ImageUrl="~/Images/calendar.png" />

                   <asp:CalendarExtender ID="calender1"  runat="server" CssClass="black" Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtdate1"  PopupButtonID="imgCalender">
             <%-- <asp:CalendarExtender ID="Calender1" runat="server" CssClass="black"
              Enabled="True" Format="dd/MM/yyyy" PopupButtonID="imgCalender" TargetControlID="Calender1">--%>
              </asp:CalendarExtender>


    
    </div>

 
  
   <br />
     
       

    <asp:Panel ID="Panel2" runat="server" Width="1000px" > 
        <div align="center">
          <asp:Label ID="lblsupplier" runat="server" CssClass="Label" Font-Bold="true" 
                                                           Font-Size="Medium" Text="Bill No:"></asp:Label>
        
               <asp:TextBox ID="txtbillno" runat="server"  AutoPostBack="true" OnTextChanged="txtbillno_TextChanged"
                                     onkeyup="return toUpper(this.id)" onkeypress="return alpha(event)" ></asp:TextBox>
                                                  
          </div>

          

          <br />
          
           <div style="width: 980px; margin: 0 auto; padding: 0" align="center">
           <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
          

              <div>

 <asp:GridView ID="gvDetails" runat="server" Width="629px" CellPadding="4" BackColor="Yellow" DataKeyNames="STransno,Invoiceno,Productcode,ProductName,Batchno,Expiredate,Quantity,Rate,SLNO"
             AllowPaging="true"  Font-Bold="true" ForeColor="Black" Font-Names="Times New Roman" Font-Size="Small" OnPageIndexChanging="GridView1_PageIndexChanging"
                      GridLines="Both"  PageSize="5" OnRowEditing="GridView1_RowEditing">
         <PagerStyle BackColor="Yellow" ForeColor="Black" HorizontalAlign="Center" Font-Size="12px"/>
         <Columns>
         <asp:TemplateField>
<ItemTemplate>
<asp:CheckBox ID="chkSelect" runat="server" />
</ItemTemplate>
</asp:TemplateField>
  <asp:TemplateField>
              <ItemTemplate>
               <asp:Button ID="AddButton" runat="server" 
       CommandName="Edit" 
CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
      Text="Edit" />
  </ItemTemplate> 
</asp:TemplateField>
</Columns>
         </asp:GridView>

</div>
           </asp:Panel></div>                          
 
      </asp:Panel>
       <asp:HiddenField ID="hfCount" runat="server" Value = "0" />
             <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="gvDetails"
        BackgroundCssClass="modal" PopupControlID="Panel5" Enabled="false">
        <Animations>
        <OnShown>
        <FadeIn Duration="0.5" Fps="50"></FadeIn>
        </OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel5" runat="server" Style="display: none;">
        <iframe id="Iframe1" src="SaleReturnStock.aspx" width="500px" height="300px" scrolling="yes"
            class="AdditionalIframe"></iframe>
         <asp:Button ID="Button1" runat="server" Text="Close"  CssClass="Buttons embossed-link" onclick="btncancel_click"/>
    </asp:Panel> 


      <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Save"  Width="95px" TabIndex="2" 
                 CssClass="Buttons embossed-link" onclick="btnsave_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" onclick="btnExit_Click"
                 CssClass="Buttons embossed-link" TabIndex="3"/>  
                </div> 
                
                </fieldset>  

</asp:Content>

