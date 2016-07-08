$(document).ready(function () {

    $('#save-meeting-registrant').click(function () {
        var $registerFrm = $('#frmSaveRegistrant');
        
        // Send the data using post
        var posting = $.post($registerFrm.attr('action'), $registerFrm.serialize());

        posting.done(function (data) {
            $('#frmSaveRegistrant').hide();
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