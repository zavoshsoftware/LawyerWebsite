function submitAppointment() {
    var fullName = $('#name').val();
    var cellNumber = $('#cell').val();
    var subject = $('#subject').val();
    var email = $('#email').val();
    var message = $('#message').val();

    if (fullName !== "" && cellNumber !== "" && message!=="") {
        $.ajax(
            {
                url: "/ConsaltantRequests/SubmitRequest",
                data: { fullName: fullName, cellNumber: cellNumber, subject: subject, email: email, message: message  },
                type: "GET"
            }).done(function (result) {
            if (result === "true") {
                $("#errorDiv").css('display', 'none');
                $("#SuccessDiv").css('display', 'block');
                localStorage.setItem("id", "");
            }
            else  {
                $("#errorDiv").html('خطایی رخ داده است. لطما مجدادا تلاش کنید.');
                $("#errorDiv").css('display', 'block');
                $("#SuccessDiv").css('display', 'none');

            }
        });
    }
    else {
        $("#errorDiv").html('فیلد های ستاره دار را تکمیل نمایید.');
        $("#errorDiv").css('display', 'block');
        $("#SuccessDiv").css('display', 'none');

    }
}