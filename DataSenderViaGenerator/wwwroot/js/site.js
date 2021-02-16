// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var generatedJson;
var jsonName;
var rowNumber;

function deleteField(event) {
    if (event.target) {
        event = event.target;
    }
    let curRow = event.parentElement.parentElement;
    let table = curRow.parentElement;
    table.removeChild(curRow);
}

function addField(event) {
    let tr = document.createElement("tr");
    tr.setAttribute("name", "generatorField");

    let tdName = document.createElement("td");
    let inputName = document.createElement("input");
    inputName.type = "text";
    inputName.style = "width:100%";
    inputName.value = "New column";
    tdName.appendChild(inputName);
    tr.appendChild(tdName);

    let tdType = document.createElement("td");
    let select = document.createElement("select");
    select.className = "form-control";
    select.innerHTML = $("#selectOptions").html();
    tdType.appendChild(select);
    tr.appendChild(tdType);
    
    let tdOptions = document.createElement("td");
    let inputBtn = document.createElement("input");
    inputBtn.type = "button";
    inputBtn.classList = "btn btn-outline-primary";
    inputBtn.value = "Delete";
    inputBtn.addEventListener("click", function (newElement) {
        deleteField(newElement)
    });

    tdOptions.appendChild(inputBtn);
    tr.appendChild(tdOptions);    
    

    $(".table>tbody")[0].appendChild(tr);
}

function generate() {
    jsonName = $("#jsonName")[0].value;
    let startIndex = $("#startIndex")[0].value;
    rowNumber = $("#rowNumber")[0].value;

    let dataArray = new Array();
    let types = new Array();

    let generatorFields = $("tr[name=generatorField]");
    for (let i = 0; i < generatorFields.length; i++) {
        let tds = generatorFields[i].getElementsByTagName("td");
        let name = [...tds[0].childNodes].filter(x => x.nodeName === "INPUT")[0].value;
        let type = [...tds[1].childNodes].filter(x => x.nodeName === "SELECT")[0].selectedOptions[0].text;
        let newType = { name, type };
        types.push(newType);
    }

    for (let i = startIndex; i < startIndex + rowNumber; i++) {
        let newObj = new Object();
        for (let j = 0; j < types.length; j++) {
            switch (types[j].type) {
                case "Integer":
                    data = i;
                    break;
                case "String":
                    data = `${types[j].name + i}`;
                    break;
                case "Date":
                    data = new Date(999999999999 + i * 10000000);
                    break;
                case "Guid":
                    data = uuidv4();
                    break;
                case "Email":
                    data = `mail${i}@example.com`;
                    break;
                case "Phone":
                    data = Math.floor(100000000 + Math.random() * 900000000);
                    break;
                case "DateAndTimeString":
                    data = new Date(999999999999 + i * 10000000).toUTCString();
                    break;
            }
            newObj[types[j].name] = data;
        }
        dataArray.push(newObj);
    }


    if (jsonName) {
        generatedData = new Object();
        generatedData[jsonName] = dataArray;
    } else {
        generatedData = dataArray;
    }

    generatedJson = JSON.stringify(generatedData);

    let inp = $("#generatedData")[0];
    inp.value = generatedJson;
    inp.style = "";

    $("#saveButton")[0].style = "";

}

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function save() {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(generatedJson));

    let fileName;

    if (jsonName) {
        fileName = `${jsonName}-${rowNumber}.json`;
    } else {
        fileName = `testdata-${rowNumber}.json`;
    }

    element.setAttribute('download', fileName);

    element.style.display = 'none';

    element.click();
}