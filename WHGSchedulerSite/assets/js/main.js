$(document).ready(function () {

    $('#save-meeting-registrant').click(function () {
        var $registerFrm = $('#frmSaveRegistrant');
        
        if (isValid($registerFrm)) {
            // Send the data using post
            var posting = $.post($registerFrm.attr('action'), $registerFrm.serialize());

            posting.done(function (data) {
                $('#frmSaveRegistrant').hide();
                $('#confirmView').show();
            });
        }

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

function isValid($form) {
    var valid = true;
    $form.find('div.form-group').removeClass('has-error');

    $(".required").each(function () {
        if (!$.trim($(this).val())) {
            $(this).parent('div.form-group').addClass('has-error');
            valid = false;
        }
    });

    return valid;
}