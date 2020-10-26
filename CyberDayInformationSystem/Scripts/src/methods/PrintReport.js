function PrintReport() {
    const panel = document.getElementsByClassName("PrintPanel");
    var printWindow = window.open("", "", "height=600,width=1000");
    printWindow.document.write("<html><head><title>Print Report</title>");
    printWindow.document.write("</head><body >");
    printWindow.document.write(panel.item(0).innerHTML);
    printWindow.document.write("</body></html>");
    printWindow.document.close();
    setTimeout(function() {
            printWindow.print();
        },
        100);
    return false;
}