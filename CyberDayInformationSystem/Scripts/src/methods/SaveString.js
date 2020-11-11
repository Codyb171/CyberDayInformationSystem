function SaveData(id) {
    const panel = document.getElementsByClassName("PrintPanel");
    debugger;
    alert(panel.item(0).innerText);
    document.getElementById(id).value = encode(panel.item(0).innerText);
    debugger;
}