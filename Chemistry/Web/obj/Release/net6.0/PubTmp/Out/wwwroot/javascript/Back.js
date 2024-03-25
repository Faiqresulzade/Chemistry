var count = 1;
console.log(1);
$(function () {
    $(document).on('click', '.loadmore', function () {
        console.log(2);

        $.ajax({
            method: "GET",
            url: "/VideoLesson/LoadMore",
            data: {
                count: count
            },
            success: function (result) {
                $("#VideoLessons").append(result);
                count++;
            },
            error: function () {
                alert("error");
            }
        })
    })
})



