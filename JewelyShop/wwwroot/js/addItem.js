function addItem() {
    let divItem = document.getElementById("addItem");
    if (divItem.style.display === "none")
        divItem.style.display = "";
    else
        divItem.style.display = "none";
}