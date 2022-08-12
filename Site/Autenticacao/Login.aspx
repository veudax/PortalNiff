<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Suportte.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" charset="utf-8" name="viewport" content="width=device-width, inicial-scale-1, maximum-scale=1"/>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <script src="http://code.jquery.com/jquery-1.11.2.min.js"></script>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/Padrao/Login.css" rel="stylesheet" />
    <link href="../Content/Padrao/Padrao.css" rel="stylesheet" />

    <title>
        <asp:Literal runat="server" Text="Suportte NIFF" />
    </title>

</head>

    <body>
        <div class="container">
            <img src="../Imagens/AvatarLogin.png" />
            <form runat="server">
                <div class="form_input">     
                    <i class="fa fa-user fa-fw" aria-hidden="true"></i>
                    <asp:TextBox ID="usuarioTextBox" runat="server"
                        Style="text-transform: uppercase" 
                        class="login"
                        placeholder="Usuário" >
                        
                    </asp:TextBox>
                </div>
                <div class="form_input">
                   <i class="fa fa-key fa-fw" aria-hidden="true"></i>
                   <asp:TextBox ID="senhaTextBox" runat="server"
                        TextMode="Password"
                        class="login"
                        placeholder="Senha">
                        
                    </asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="entrarButton" runat="server" Text="Acessar" class="botaologin"  OnClick="entrarButton_Click1" />
                    <br/>
                    <a href="#">Esqueceu sua senha?</a>
                </div>
            </form>
        </div>

    </body>
</html>
