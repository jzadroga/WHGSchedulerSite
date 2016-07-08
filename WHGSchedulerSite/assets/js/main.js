$(document).ready(function () {

    $('#save-meeting-registrant').click( function() {
        $('#frmSaveRegistrant').hide();

        // Send the data using post
        var posting = $.post($('#frmSaveRegistrant').attr('action'), { s: 'tt' });

        posting.done(function (data) {
            $('#confirmView').show();
            console.log(data);
        });

        return false;
    });

    $('.register-meeting').click(function () {
        $('#frmSaveRegistrant').show();
        $('#confirmView').hide();

        $('#frmSaveRegistrant .form-control').val('');
        $('#meetingID').val($(this).attr('data-id'));
        $('#sponsorID').val($(this).attr('data-sponsor'));

        $('#mdl-register').modal('show');

        return false;
    });
});