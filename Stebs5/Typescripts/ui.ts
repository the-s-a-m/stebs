﻿module Stebs {

    var ctx: CanvasRenderingContext2D;
    var canvas: HTMLCanvasElement;

    export var ui = {

        private editorContentChanged: false,

        /**
         * Sets the flag, if stebs thinks the editor content is changed.
         */
        setEditorContentChanged(value: boolean) {
            ui.editorContentChanged = value;
            $('#filename-star').css('display', value ? 'inline' : 'none');
        },

        /**
         * Returns if the editor content is flaged as changed.
         */
        isEditorContentChanged(): boolean {
            return ui.editorContentChanged;
        },

        /**
         * Stores a global reference of the canvas and sets the global style.
         */
        setupCanvas(): void {
            canvas = <HTMLCanvasElement>$('#canvas')[0];
            ctx = canvas.getContext('2d');
            this.normalizeCanvas();

            ctx.font = '20pt Helvetica';
            ctx.textAlign = 'center';
        },

        /**
         * Resize canvas to real size (otherwise the content gets stretched).
         */
        normalizeCanvas(): void {
            var width = parseInt($('#canvas').css('width'), 10);
            var height = parseInt($('#canvas').css('height'), 10);
            if (canvas.width != width || canvas.height != height) {
                canvas.width = width;
                canvas.height = height;
            }
        },

        /**
         * Sets the width of #codingView to a prozentual value.
         * This allows correct browser resizing without additional client code.
         */
        setCodingViewWidth(): void {
            var width = (visible.architecture ? ' - ' + widths.architecture : '') + (visible.devices ? ' - ' + widths.devices : '');
            $('#codingView').css('width', 'calc(100% - ' + widths.sidebar + ' ' + width + ')');
        },

        /**
         * Opens/Closes the devices sidebar.
         */
        toggleDevices(): void {
            var animation = { left: (visible.devices ? '-=' : '+=') + widths.devices };
            $('#devices, #architecture').animate(animation);
            var animation2 = { left: animation.left, width: (visible.devices ? '+=' : '-=') + widths.devices };
            $('#codingView').animate(animation2, ui.setCodingViewWidth);
            visible.devices = !visible.devices;
        },

        /**
         * Opens/Closes the architecture sidebar.
         */
        toggleArchitecture(): void {
            var animation = { left: (visible.architecture ? '-=' : '+=') + widths.architecture };
            $('#architecture').animate(animation);
            var animation2 = { left: animation.left, width: (visible.architecture ? '+=' : '-=') + widths.architecture };
            $('#codingView').animate(animation2, ui.setCodingViewWidth);
            visible.architecture = !visible.architecture;
        },

        /**
         * Sets the width of #codingFrame to a prozentual value.
         * This allows correct browser resizing without additional client code.
         */
        setCodingFrameHeight(): void {
            var height = (visible.output ? ' - ' + heights.output : '');
            $('#codingFrame').css('height', 'calc(100% - ' + heights.bars + ' - ' + heights.runAndDebug + height + ')');
        },

        /**
         * Opens/Closes the output bar.
         */
        toggleOutput(): void {
            outputView.refresh();
            $('#codingFrame').animate({ height: (visible.output ? '+=' : '-=') + heights.output }, ui.setCodingFrameHeight);
            visible.output = !visible.output;
            if (visible.output) { $('.output-container').slideDown(); }
            else { $('.output-container').slideUp(() => outputView.refresh()); }
        },

        openOutput(): void {
            if (!visible.output) { this.toggleOutput(); }
        },

        showOutput(text: string): void {
            outputView.getDoc().setValue(text);
        },

        /**
         * Reads the selected step size from the radio buttons.
         */
        getStepSize(): SimulationStepSize {
            if ($('#instructionStepSpeed').prop('checked')) { return SimulationStepSize.Instruction; }
            else if ($('#macroStepSpeed').prop('checked')) { return SimulationStepSize.Macro; }
            else { return SimulationStepSize.Micro; }
        },

        /**
        * Highlight the given line
        */
        highlightLine(ipNr: number): void {
            var linenr = Stebs.ramContent.getLineNr(ipNr);
            Stebs.codeEditor.getDoc().setCursor({ ch: 0, line: linenr });
        }

    };
}
