$(document).ready(function () {
    var SpeechRecognition = SpeechRecognition || webkitSpeechRecognition;
    var SpeechRecognitionEvent = SpeechRecognitionEvent || webkitSpeechRecognitionEvent;
    var recognition = new SpeechRecognition();
    var synth = window.speechSynthesis;
    var utterance = new SpeechSynthesisUtterance();
    //recognition.continuous = false;
    recognition.lang = 'en-us';
    recognition.interimresults = false;
    recognition.maxalternatives = 1;
    recognition.start();

    recognition.onresult = function (event) {
        $('#type-action-input').val(event.results[0][0].transcript);
        var keyevent = $.Event("keypress");
        keyevent.which = 13;
        $('#type-action-input').trigger(keyevent);
    };

    recognition.onend = function () { if (!synth.speaking) recognition.start(); };

    function updatePlayerData(player) {
        $.each(player, function (i, e) {
            $('#Hero_' + i).html(e);
        });
        $('#Hero_Exp_ExpToLvlUp').find('.progress-bar').width(player.Exp/player.ExpToLvlUp*100+'%');
        $('#Hero_AHP_MHP').find('.progress-bar').width(player.AHP / player.MHP * 100 + '%');
        $('#Hero_AMP_MMP').find('.progress-bar').width(player.AMP / player.MMP * 100 + '%');
        $('#Hero_MinDmg_MaxDmg').html(player.MinDmg + ' - ' + player.MaxDmg);
        let items = '';
        $.each(player.Pockets, function (i, e) {
            items += '<div class="col-12" tittle="'+e.Item.ItemInfo.Description+'">' + e.Item.Quantity + ' ' + e.Item.ItemInfo.Name + '</div>';
        });
        $('#items-container').html(items);
    }

    function setupUtterance(msg) {
        utterance = new SpeechSynthesisUtterance(msg);
        utterance.onend = function () { recognition.start(); };
        utterance.lang = "en-US";
        synth.speaking = true;
        utterance.onstart = function () {
            recognition.stop();
        };
    }

    function readMessage(msg) {
        synth.cancel();
        setupUtterance(msg);
        synth.speak(utterance);
    }

    $('#type-action-input').on('keypress', function (e) {

        if (e.which === 13) {

            $(this).attr('disabled', 'disabled');
            if ($(this).val().startsWith("repeat")) {
                synth.cancel();
                setupUtterance(utterance.text);
                synth.speak(utterance);
            } else {
                $.post({
                    url: '/Gameplay/ExecuteAction',
                    data: {
                        action: $(this).val(),
                        heroId: $('#ID').val()
                    }
                }).then(function (jsonResult) {
                    let result = JSON.parse(jsonResult);
                    let msg = "";
                    $.each(result.Messages, function (i, e) {
                        $('#events-container').append('<div class=\"col-12\">' + e + '</div>');
                        msg += e + "\r\n";
                    });
                    updatePlayerData(result.Player);
                    readMessage(msg);
                 });

            }
            $(this).val('');
            $(this).removeAttr('disabled');
            $('.card-body').scrollTop = $(this).closest('.card-body').first().scrollHeight;
        }
    });

    $('.delete-button').on('click', function () {
        let $this = $(this);
        $($this.data('target')).find('#confirm-modal').data('url', $this.data('href')).data('id', $this.data('id'));
    });

    $('#confirm-modal').on('click', function () {
        let $this = $(this);
        $.ajax({
            url: $this.data('url'),
            type: 'DELETE',
            data: {
                heroId: $this.data('id')
            },
            statusCode: {
                200 : function(response) {
                    $('#character-' + $this.data('id')).remove();
                }
            }
        });
    });
});