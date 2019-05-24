// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myFunction() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}

function drop() {
    /* Loop through all dropdown buttons to toggle between hiding and showing its dropdown content - This allows the user to have multiple dropdowns without any conflict */
    var dropdown = document.getElementsByClassName("dropdown-btn");
    var i;

    for (i = 0; i < dropdown.length; i++) {
        dropdown[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var dropdownContent = this.nextElementSibling;
            if (dropdownContent.style.display === "block") {
                dropdownContent.style.display = "none";
            } else {
                dropdownContent.style.display = "block";
            }
        });
    }
}
drop()

function getRowId(caller, id) {
    document.getElementById(id).value = caller.parentElement.parentElement.id;
}

function dismark_selection() {
    var op = doc.getElementsByTagName('option');
    for (var i = 0; i < op.length; ++i)
        op[i].selected = false;
}

function clean_selects() {
    var op = document.getElementsByTagName('option');
    for (var i = 0; i < op.length; ++i)
        op[i].selected = false;
}

function oneOfAllAtLeast() {
    var sel = document.getElementsByTagName('select');
    sel[0].required = false;
    sel[1].required = false;

    var op = document.getElementsByTagName('option');
    var count = 0;
    for (var i = 0; i < op.length; ++i)
        if (op[i].selected)
            ++count;
    if (count === 0)
        sel[0].required = true;
}

function closeDialog() {
    var dialog = document.getElementById("rzn");
    document.getElementById('mot').value = document.getElementById('msg').value;
    dialog.close();
}
