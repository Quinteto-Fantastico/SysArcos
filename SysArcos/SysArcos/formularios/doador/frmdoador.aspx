﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="frmdoador.aspx.cs" Inherits="ProjetoArcos.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <div class="entidade">
        Doador
    </div>
    <div class="acao">
        <asp:Label ID="lblAcao" runat="server" Text="NOVO"></asp:Label>
    </div>
    <div>
        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
    </div>
    <div class="row">
        <div class="col-12 col-lg-10 row_fields">
            <asp:Label ID="Label2" runat="server" Text="Nome do doador"></asp:Label>
        </div>
        <div class="col-lg-4 col-md-12">
            <asp:TextBox ID="txt_nomedoador" class="form-control"  runat="server" Width="100%" MaxLength="50" Height="30px"></asp:TextBox>
        </div>
    </div>    
    <div  class="row">
        <div class="col-12 col-lg-10 row_fields">
            <asp:Label ID="Label10" runat="server" Text="Logradouro"></asp:Label>
        </div>
        <div class="col-lg-4 col-md-12">
            <asp:TextBox ID="txt_logradouro" class="form-control" runat="server" Width="100%" MaxLength="14" Height="30px"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-12 col-lg-10 row_fields">
            <asp:Label ID="Label4" runat="server" Text="Número:"></asp:Label>
        </div>
        <div class="col-lg-4 col-md-12">
            <asp:TextBox ID="txt_numero" class="form-control" runat="server" Width="100%" MaxLength="10" Height="30px"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-12 col-lg-10 row_fields">
            <asp:Label ID="Label5" runat="server" Text="Bairro:" ToolTip="  "></asp:Label>
        </div>
        <div class="col-lg-4 col-md-12">
            <asp:TextBox ID="txt_bairro" class="form-control" runat="server" Width="100%" MaxLength="30" Height="30px"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-12 col-lg-10 row_fields">
            <asp:Label ID="Label6" runat="server" Text="CEP:"></asp:Label>
        </div>
        <div class="col-lg-4 col-md-12">
            <asp:TextBox ID="txt_CEP" class="form-control" runat="server" MaxLength="9" Height="30px" Width="100%"  Placeholder="99999-999"  onkeydown="mascara( this,CEP  );"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-12 col-lg-10 row_fields">
            <asp:Label ID="Label7" runat="server" Text="Cidade:"></asp:Label>
        </div>
        <div class="col-lg-4 col-md-12">
            <asp:TextBox ID="txt_cidade" class="form-control" runat="server" MaxLength="40" Height="30px" Width="100%"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-12 col-lg-10 row_fields">
            <asp:Label ID="Label8" runat="server" Text="Estado:"></asp:Label>
        </div>
        <div class="col-12 col-lg-4">
            <asp:DropDownList ID="drp_estado" class="form-control" runat="server" Height="30px" Width="100%">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>AC</asp:ListItem>
                <asp:ListItem>AL</asp:ListItem>
                <asp:ListItem>AP</asp:ListItem>
                <asp:ListItem>AM</asp:ListItem>
                <asp:ListItem>BA</asp:ListItem>
                <asp:ListItem>CE</asp:ListItem>
                <asp:ListItem>DF</asp:ListItem>
                <asp:ListItem>ES</asp:ListItem>
                <asp:ListItem>GO</asp:ListItem>
                <asp:ListItem>MA</asp:ListItem>
                <asp:ListItem>MT</asp:ListItem>
                <asp:ListItem>MS</asp:ListItem>
                <asp:ListItem>MG</asp:ListItem>
                <asp:ListItem>PA</asp:ListItem>
                <asp:ListItem>PB</asp:ListItem>
                <asp:ListItem>PR</asp:ListItem>
                <asp:ListItem>PE</asp:ListItem>
                <asp:ListItem>PI</asp:ListItem>
                <asp:ListItem>RJ</asp:ListItem>
                <asp:ListItem>RN</asp:ListItem>
                <asp:ListItem>RS</asp:ListItem>
                <asp:ListItem>RO</asp:ListItem>
                <asp:ListItem>RR</asp:ListItem>
                <asp:ListItem>SC</asp:ListItem>
                <asp:ListItem>SP</asp:ListItem>
                <asp:ListItem>SE</asp:ListItem>
                <asp:ListItem>TO</asp:ListItem>
            </asp:DropDownList>
        </div>       
    </div>
    <div class="row">
        <div class="col-12 col-lg-10 row_fields">
            <asp:Label ID="Labe20" runat="server" Text="Disponibilidade"></asp:Label>
        </div>
        <div class="col-12 col-lg-4">
            <asp:TextBox ID="txt_disponibilidade"  class="form-control" runat="server" MaxLength="50" Width="100%" Height="30px"></asp:TextBox>
        </div>
        <div class="col-12 col-lg-10 row_fields">
            <asp:Label ID="Label12" runat="server" Text="Tipo de doação"></asp:Label>
        </div>
        <br/>
        <div class="col-12 col-lg-4">
            <asp:TextBox ID="txt_tipodoacao" runat="server" Width="100%" class="form-control"></asp:TextBox>
        </div>
        <br/>
        <div class="col-12 col-lg-12">
            <asp:CheckBox ID="cb_ativo" runat="server" Text="Ativo" Checked="True" />
        </div>
        <br/>
    </div>
    <div class="row">
        <div class="col-12 col-lg-4 row_buttons">
            <asp:Button ID="btn_novo" class="btn btn-primary" runat="server" Text="Novo" Font-Bold="True" Width="100%" Font-Size="X-Large" OnClick="btn_novo_Click" />
        </div>
        <div class="col-12 col-lg-4 row_buttons">    
            <asp:Button ID="btnCadastra" class="btn btn-primary" runat="server" OnClick="btnCadastra_Click" Text="Salvar" Font-Bold="True" Width="100%" Font-Size="X-Large" />
        </div>
        <div class="col-12 col-lg-4 row_buttons">
            <asp:Button ID="btnConsulta" class="btn btn-primary" runat="server" Text="Buscar" Font-Bold="True" Width="100%" OnClick="btnConsulta_Click" Font-Size="X-Large" />
        </div>
        <br/>
        <br/>
    </div>
</asp:Content>
