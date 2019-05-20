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

function add(pais) {
    if (validate()) {
        var destiny = document.getElementById("all");
        var idx = destiny.getElementsByTagName("li").length;

        var obj = make_a_new_container(pais, idx)

        destiny.appendChild(obj);
    }
}

function make_a_new_container(pais, idx) {
    var li = document.createElement("li"); {
        var p = document.createElement("input"); {
            p.value = pais;
            p.readOnly = "readonly";
            p.style = "border: 0; font-size: 30px; text-align: center;";
            p.name = "Country[" + idx + "]";
        }
        li.appendChild(p);
        var fc = document.createElement("div"); {
            fc.class = "form-group";
            var l = document.createElement("label"); {
                l.htmlFor = "City[" + idx + "]";
                l.innerHTML = "Ciudad de destino:";
            }
            fc.appendChild(l);
            var City = document.createElement("input"); {
                City.className = "form-control col-md-3 col-md-offset-10";
                City.type = "text";
                City.required = true;
                City.id = "City[" + idx + "]";
                City.name = "City[" + idx + "]";
                City.dataset = "data-val='true'; data-val-required='The City field is required.';";
            }
            fc.appendChild(City);
            var s = document.createElement("label"); {
                s.htmlFor = "Start[" + idx + "]";
                s.innerHTML = "Fecha de partida:";
            }
            fc.appendChild(s);
            var sdate = document.createElement("input"); {
                sdate.className = "form-control col-md-3 col-md-offset-10";
                sdate.type = "datetime-local";
                sdate.required = true;
                sdate.id = "Start[" + idx + "]";
                sdate.name = "Start[" + idx + "]";
                sdate.dataset = "data-val='true'; data-val-required='The Start field is required.';";
            }
            fc.appendChild(sdate);
            /*var sp = document.createElement("span"); {
                sp.className = "text-danger field-validation-valid";
                sp.dataset = "data-valmsg-for='Start'; data-valmsg-replace='true';";
            }
            fc.appendChild(sp);*/
            var e = document.createElement("label"); {
                e.htmlFor = "End[" + idx + "]";
                e.innerHTML = "Fecha de llegada:";
            }
            fc.appendChild(e);
            var edate = document.createElement("input"); {
                edate.className = "form-control col-md-3 col-md-offset-3";
                edate.type = "datetime-local";
                edate.required = true;
                edate.id = "End[" + idx + "]";
                edate.name = "End[" + idx + "]";
                edate.dataset = "data-val='true'; data-val-required='The Start field is required.';";
            }
            fc.appendChild(edate);
            var m = document.createElement("label"); {
                m.htmlFor = "Motivo[" + idx + "]";
                m.innerHTML = "Motivo(Opcinal):";
            }
            fc.appendChild(m);
            var Motivo = document.createElement("input"); {
                Motivo.className = "form-control col-md-4 col-md-offset-3";
                Motivo.type = "text";
                Motivo.id = "Motivo[" + idx + "]";
                Motivo.name = "Motivo[" + idx + "]";
            }
            fc.appendChild(Motivo);
            var but = document.createElement("button"); {
                but.className = "button";
                but.type = "button";
                but.addEventListener("click", function () {
                    this.parentNode.parentNode.parentNode.removeChild(this.parentNode.parentNode);
                });
                var sp = document.createElement("span"); {
                    sp.innerHTML = "Remove";
                }
                but.appendChild(sp);
            }
            fc.appendChild(but);
        }
        li.appendChild(fc);
    }
    return li;
}


function validate() {
    var input = document.getElementById("pais");
    
    var optionFound = false;
    var datalist = input.list;
    for (var j = 0; j < datalist.options.length; j++) {
        if (input.value === datalist.options[j].value) {
            optionFound = true;
            break;
        }
    }
    input.value = '';
    if (optionFound) {
        input.placeholder = 'Selecciona un pais';
        return true;
    } else {
        input.placeholder = 'Selecciona un pais valido';
        return false;
    }
}
