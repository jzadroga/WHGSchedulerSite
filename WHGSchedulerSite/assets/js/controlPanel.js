$(document).ready(function () {
    $('.date').datetimepicker();

    $('.edit-meeting').click(function (event) {
        var meeting = {
            id: $(this).attr('data-id'),
            start: $(this).attr('data-start'),
            end: $(this).attr('data-end'),
            requests: $(this).attr('data-requests')
        };

        $('#meetingID').val(meeting.id);
        $('#meetingStartDate').val(meeting.start);
        $('#meetingEndDate').val(meeting.end);
        $('#meetingRequests').val(meeting.requests);

        $('#mdl-meeting').modal('show');
    });

    $('.edit-sponsor').click(function (event) {
        var sponsor = {
            name: $(this).attr('data-name'),
            id: $(this).attr('data-id'),
            logo: $(this).attr('data-logo'),
            url: $(this).attr('data-url'),
            description: $(this).attr('data-desc')
        };

        if (sponsor.id === '0') {
            $('#logoView').hide();
            $('#sponsorLogoFile').show();
        } else {
            $('#logoView').show();
            $('#sponsorLogoFile').hide();
        }

        $('#sponsorName').val(sponsor.name);
        $('#sponsorID').val(sponsor.id);
        $('#sponsorLogo').val(sponsor.logo);
        $('#sponsorUrl').val(sponsor.url);
        $('#sponsorDescription').val(sponsor.description);

        $('#mdl-sponsor').modal('show');
    });

    $('#modifiyLogo').click(function (event) {
        $('#logoView').hide();
        $('#sponsorLogoFile').show();
        return false;
    });
});