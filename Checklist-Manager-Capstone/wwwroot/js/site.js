﻿let counter = 1

$("#addLineItem").on("click", evt => {
    let lineItems = ""
        lineItems +=`
        <div class='form-group'>
            <label name="ChecklistLineItems[${counter}]" class="control-label"></label>
            <input name="ChecklistLineItems[${counter}]" class="form-control" />
        </div>`
        $("#newLineItems").append(lineItems)
    counter += 1
})

$("#submitNewChecklist").on("click", evt => {
    console.log("Submit is working")
})