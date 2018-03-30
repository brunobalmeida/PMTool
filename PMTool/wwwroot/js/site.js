// Write your JavaScript code.
jQuery.extend(jQuery.validator.methods, {
    date: function (value, element) {
        var isChrome = window.chrome;
        // make correction for chrome
        if (isChrome) {
            var d = new Date();
            return this.optional(element) ||
                !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
        }
        // leave default behavior
        else {
            return this.optional(element) ||
                !/Invalid|NaN/.test(new Date(value));
        }
    }
});



// progressbar.js@1.0.0 version is used
// Docs: http://progressbarjs.readthedocs.org/en/1.0.0/

var bar = new ProgressBar.SemiCircle(progressSemiCircle, {
    strokeWidth: 6,
    color: '#FFEA82',
    trailColor: '#eee',
    trailWidth: 1,
    easing: 'easeInOut',
    duration: 1400,
    svgStyle: null,
    text: {
        value: '',
        alignToBottom: false
    },
    from: { color: '#FFEA82' },
    to: { color: '#ED6A5A' },
    // Set default step function for all animate calls
    step: (state, bar) => {
        bar.path.setAttribute('stroke', state.color);
        bar.value = parseFloat(document.getElementById("semiCircleValue").value);
        var value = Math.round(bar.value * 100);
        if (value === 0) {
            bar.setText('');
        } else {
            bar.setText(value);
        }

        bar.text.style.color = state.color;
    }
});
bar.text.style.fontFamily = '"Raleway", Helvetica, sans-serif';
bar.text.style.fontSize = '2rem';

bar.animate(bar.value);  // Number from 0.0 to 1.0