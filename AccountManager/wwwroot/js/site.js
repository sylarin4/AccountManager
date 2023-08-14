// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    const searchInput = document.getElementById("searchInput");
    const exportButton = document.getElementById("export-button");
    const tableRows = document.querySelectorAll(".account-row");

    searchInput?.addEventListener("input", function () {
    const searchValue = searchInput.value.toLowerCase();

        if (searchValue.length >= 3) {
            tableRows.forEach(function (row) {
                const rowData = row.dataset.accountData.toLowerCase();

                if (rowData.includes(searchValue)) {
                    row.style.display = "table-row";
                } else {
                    row.style.display = "none";
                }
            });
        } else {
            tableRows.forEach(function (row) {
                row.style.display = "table-row";
            });
        }
    });

    exportButton?.addEventListener("click", function () {
        const xmlData = createXmlFromTable();
        downloadXmlFile(xmlData, "accounts.xml");
    });
});

function createXmlFromTable() {
    var tableRows = document.querySelectorAll(".account-row");
    var xml = '<?xml version="1.0" encoding="utf-8"?><accounts>';

    tableRows.forEach(function (row) {
        if (row.style.display !== "none") {
            var rowData = row.getAttribute("data-account-data");
            var [id, username, firstname, lastname, birthdate, residence, birthplace] = rowData.split(",");

            xml += `
                <account>
                    <id>${id}</id>
                    <username>${username}</username>
                    <firstname>${firstname}</firstname>
                    <lastname>${lastname}</lastname>
                    <birthdate>${birthdate}</birthdate>
                    <residence>${residence}</residence>
                    <birthplace>${birthplace}</birthplace>
                </account>
            `;
        }
    });

    xml += '</accounts>';
    return xml;
}

function downloadXmlFile(data, filename) {
    var blob = new Blob([data], { type: 'application/xml' });
    var url = URL.createObjectURL(blob);
    var a = document.createElement('a');
    a.href = url;
    a.download = filename;
    a.click();
}