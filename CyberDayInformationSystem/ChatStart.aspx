<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChatStart.aspx.cs" Inherits="CyberDayInformationSystem.ChatStart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="lib/font-awesome/css/all.css" rel="stylesheet" />
    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">
    <body>
        <div class="page-content page-container" id="page-content">
            <div class="padding">
                <div class="row container d-flex justify-content-center">
                    <div class="col-md-6">
                        <div class="card card-bordered">
                            <div class="card-header">
                                <h4 class="card-title">CyberDay Chat</h4>
                            </div>
                            <div class="ps-container ps-theme-default ps-active-y" id="chat-content" style="overflow-y: scroll !important; height: 400px !important;"></div>
                            <div class="media media-chat">
                                <div class="media-body">
                                    <input type="hidden" id="displayname" />
                                    <ul id="discussion">
                                    </ul>

                                    <div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;">
                                        <div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div>
                                    </div>
                                    <div class="ps-scrollbar-y-rail" style="top: 0px; height: 0px; right: 2px;">
                                        <div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 2px;"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="publisher bt-1 border-light">
                                <img class="avatar avatar-xs" src="https://img.icons8.com/color/36/000000/administrator-male.png" alt="...">
                                <input class="publisher-input" id="message" type="text" placeholder="Write something">
                                <div class="wrapper">
                                    <span class="publisher-btn file-group"><i class="fa fa-paperclip file-browser"></i>
                                        <input type="file">
                                    </span>
                                    <a class="publisher-btn" href="#" data-abc="true" data-emojiable="true"><i class="fa fa-smile"></i></a>
                                    <a class="btn btn-sm" href='#' type="button" id="sendmessage"><i class="fa fa-paper-plane"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="scripts/jquery-3.5.1.min.js"></script>
        <script src="scripts/jquery.signalR-2.2.2.min.js"></script>
        <script src="/signalr/hubs"></script>
        <script src="https://cdn.jsdelivr.net/npm/emoji-button@latest/dist/index.min.js"></script>
        <script type="text/javascript">

            var input = document.querySelector('.fa-smile');
            var text = document.querySelector('.publisher-input');
            var picker = new EmojiButton({
                position: 'right-start'
            })

            picker.on('emoji', function (emoji) {
                text.value += emoji;
            })

            input.addEventListener('click', function () {
                picker.pickerVisible ? picker.hidePicker() : picker.showPicker(text);
            })

            $(function () {
                var chat = $.connection.chatHub;
                chat.client.broadcastMessage = function (name, message) {
                    var encodedName = name;
                    var encodedMessage = message;

                    $("div.ps-container").append('<li><strong>' + encodedName
                        + '</strong>:&nbsp;&nbsp;' + encodedMessage + '</li>');
                };
                $('#displayname').val(prompt('Enter your chat name:', ''));
                $('#message').focus();
                $.connection.hub.start().done(function () {
                    $('#sendmessage').click(function () {
                        chat.server.send($('#displayname').val(), $('#message').val());
                        $('#message').val('').focus();
                    });
                });
            });

        </script>
    </body>
    </html>
</asp:Content>
