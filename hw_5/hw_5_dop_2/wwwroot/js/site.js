$(".js--course-card__subscribe-btn").on("click", function (e) {
    let id = $(e.target).data('course-id');
    console.log(id);
    
    $("#subscribeModal #formSubscribe input[name='courseId']").val(id);
})