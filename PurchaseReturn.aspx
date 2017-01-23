<%@ Page Title="" Language="C#" MasterPageFile="~/Pharmacy.master" AutoEventWireup="true" CodeFile="PurchaseReturn.aspx.cs" Inherits="PurchaseReturn" %>
<%@ MasterType VirtualPath="~/Pharmacy.master" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"%>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
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
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <fieldset class="BigFieldSet">
    <legend class="BigLegend">
    <h1 style="position: relative; right: 0px;">
     Purchase Return
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

 <div align="center">
     <asp:RadioButtonList ID="rdtrans" runat="server" CssClass="CheckBox" 
         RepeatDirection="Horizontal" 
         onselectedindexchanged="rdtrans_SelectedIndexChanged" AutoPostBack="true">
      <asp:ListItem>Supplier</asp:ListItem>
      <asp:ListItem>Manufacturer</asp:ListItem>
     </asp:RadioButtonList>

  </div>
  
   <br />


     
       

   <asp:Panel ID="Panel2" runat="server" Width="1000px" > 

   <div align="center">
        
          <asp:Label ID="lblsupplier" runat="server" CssClass="Label" Font-Bold="true" 
             Font-Size="Medium" Text="Supplier Name:"></asp:Label>
      
               <asp:DropDownList ID="ddlsupplier" runat="server" AutoPostBack="true" 
                height="25px" CssClass="DropDown"   Width="150px" 
                onselectedindexchanged="ddlsupplier_SelectedIndexChanged">
              </asp:DropDownList>
             
               <asp:Label ID="lbldate" runat="server" Text="Date:" CssClass="Label"></asp:Label>
    &nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtda" runat="server"  Width="75px" AutoPostBack="true" ontextchanged="txtda_TextChanged"  
                                    ></asp:TextBox>
                                      <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtda" Format="dd/MM/yyyy" runat="server">
                             </asp:CalendarExtender>
                             

                                               </div>
                                                 
                                 
 
     </asp:Panel>


       <asp:Panel ID="Panel3" runat="server" Width="1000px" > 
        <div align="center">
          <asp:Label ID="Label1" runat="server" CssClass="Label" Font-Bold="true" 
                                                           Font-Size="Medium" Text="Manufacture Name:"></asp:Label>
        
               <asp:DropDownList ID="ddlmanufacturer" runat="server" AutoPostBack="true" height="25px" OnSelectedIndexChanged="ddlmanufacturer_SelectedIndexChanged"
                             CssClass="DropDown" Width="150px" >
              </asp:DropDownList>


              <asp:Label ID="lbl" runat="server" Text="Date:" CssClass="Label"></asp:Label>
    &nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txtdate10" runat="server" CssClass="TextBox" width="75px"  AutoPostBack="true" OnTextChanged="txtdate10_TextChanged"
                 onkeypress="return alpha1(event)"></asp:TextBox>

                  <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtdate10" Format="dd/MM/yyyy" runat="server">
                            </asp:CalendarExtender>
                            


                                                
                                                  
          </div> 
          
         
      </asp:Panel>

       <div align="left" class="SubmitButtons" style="position:relative; left: 200px; top: 0px;"> 


        <asp:Button ID="Button1" runat="server" Text="Generate"  Width="95px" 
                 CssClass="Buttons embossed-link" onclick="Button1_Click"/>                         
     </div>

     <br />
     <br />

     </fieldset>


      <fieldset class="BigFieldSet">


       <asp:Panel ID="Panel4" runat="server" Width="1000px" >

        <div align="center">
          


              <asp:Label ID="Label4" runat="server" Text="Expired Date:" CssClass="Label"></asp:Label>
    &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="dddate" runat="server" Width="100px"  AutoPostBack="true" CssClass="DropDown"></asp:DropDownList>

            <br />
            <br />

                

        <asp:GridView ID="Gridreturn" runat="server" Width="629px" CellPadding="4" BackColor="Yellow"
             AllowPaging="true"  Font-Bold="true" ForeColor="Black" Font-Names="Times New Roman" Font-Size="Small"
                      GridLines="Both" AutoGenerateColumns="false" PageSize="5">

                       <Columns>
            <asp:BoundField DataField="ProductName" HeaderText="Product Name" ItemStyle-Width="150" />
            <asp:BoundField DataField="Batchid" HeaderText="Batch No" ItemStyle-Width="40" />
             <asp:BoundField DataField="Invoiceno" HeaderText="Invoiceno" ItemStyle-Width="40" />
            <asp:BoundField DataField="Expiredate" HeaderText="Expiry Date" ItemStyle-Width="60" />
            <asp:BoundField DataField="PStockinhand" HeaderText="Stockinhand" ItemStyle-Width="50" />
            <asp:BoundField DataField="Purchaseprice" HeaderText="Purchaseprice" ItemStyle-Width="50" />
            <asp:BoundField DataField="ManufactureName" HeaderText="ManufactureName" ItemStyle-Width="50" />
              <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ItemStyle-Width="50" />
               <asp:BoundField DataField="Productinhand" HeaderText="Productinhand" ItemStyle-Width="50" />

            
            
             <asp:TemplateField HeaderText="Return_Qty">
<ItemTemplate>
<asp:TextBox ID="Return_Qty" runat="server" BackColor="Orange" Text='<%# Eval("ReturnQuantity") %>' Width="50" AutoPostBack="true"  OnTextChanged="Return_Qty_TextChanged"/>
</ItemTemplate>
</asp:TemplateField>

            <asp:BoundField DataField="CurrentDate" HeaderText="CurrentDate" ItemStyle-Width="50" />
        </Columns>

         
         </asp:GridView>
         </div>

         
         
        <table align="right">
                 <tr>
                   <td>
              <div style="float:right; width:70px">
              <asp:Label ID="lblamount" runat="server" Text="Amount:" Font-Size="Medium" Font-Bold="true" CssClass="Label" ></asp:Label>
              <asp:Image ID="image3" runat="server" ImageUrl="~/Images/rupees.JPG" />
              </div>
              </td>
              <td>
              <asp:TextBox ID="txtamount" width="80px" runat="server"></asp:TextBox>
                  
                     </td>
                  </tr>
                  </table>
       
        <br />
        <br />




     
      
      
      <div align="left" class="SubmitButtons" style="position:relative; left: 450px; top: 0px;"> 
             <asp:Button ID="btnsave" runat="server" Text="Save"  Width="95px"  
                 CssClass="Buttons embossed-link" onclick="btnsave_Click"/>
              <asp:Button ID="btnExit" runat="server" Text="Exit" Width="95px" 
                 CssClass="Buttons embossed-link"  onclick="btnExit_Click1"/>  
                </div> 
                
                  </asp:Panel>  
               
 </fieldset>


 


</asp:Content>

