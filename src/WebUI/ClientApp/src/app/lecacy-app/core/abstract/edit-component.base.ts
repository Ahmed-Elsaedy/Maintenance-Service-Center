import { OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { SidePanelService } from '../services/side-panel.service';
declare var $: any;

export class EditComponentBase implements OnInit, AfterViewInit, OnDestroy {
    protected subs: Subscription[] = [];
    formGroup: FormGroup;
    selectFieldKeys: string[] = [];

    constructor(protected sidePanel: SidePanelService) {
    }

    ngOnInit(): void {
        this.subs.push(this.sidePanel.panelOpened.subscribe(e => this.onSidePanelOpened(e)));
    }

    onSidePanelOpened(e) {
    }

    ngAfterViewInit(): void {
        // initialize select2
        $('.modal select.form-control').select2();

        // set default selected values
        for (const key of Object.keys(this.formGroup.controls)) {
            if (this.selectFieldKeys.includes(key)) {
                var value = this.formGroup.controls[key].value;

                if (value) {
                    var selector = '#' + key;
                    if (value instanceof Array == false) {
                        var arr = [];
                        arr.push(value);
                        value = arr;
                    }
                    value.forEach(x => {
                        if ($(selector).find("option[value='" + x + "']").length) {
                            $(selector).val(x).trigger('change');
                        } else {
                            var newOption = new Option(x, x, true, true);
                            $(selector).append(newOption).trigger('change');
                        }
                    });
                }
            }
        }
        // add events trigger when changed
        $('select').on('select2:select', e => this.onSelect2Change(e));
        $('select').on('select2:close', e => this.onSelect2Change(e));
    }

    onSelect2Change(e) {
        var controlName = $(e.target).attr('id');
        var formControl = this.formGroup.get(controlName);
        if (formControl) {
            formControl.setValue($(e.target).val());
            var control = this.formGroup.get(controlName);
            this.updateSelect2Style(controlName, control.valid);
        }
    }

    appendSelect2Option(id, text, value, selected) {
        var newOption = new Option(text, value, true, selected)
        var append = $('#' + id).append(newOption);
        if (selected)
            append.trigger('change');
    }

    emptySelect2(id: string) {
        $('#' + id).empty();
    }

    onSubmit() {
        if (this.formGroup.invalid)
            this.onInvalidSubmit();
        else
            this.onValidSubmit();
    }

    updateSelect2Style(controlName, valid) {
        if (valid)
            $('span[aria-labelledby="select2-' + controlName + '-container"]').css('border', '1px solid #ccc');
        else
            $('span[aria-labelledby="select2-' + controlName + '-container"]').css('border', '1px solid red');
    }

    onInvalidSubmit() {
        this.formGroup.markAllAsTouched();
        this.selectFieldKeys.forEach(key => {
            if (this.formGroup.controls[key])
                this.updateSelect2Style(key, this.formGroup.controls[key].valid)
        });

        for (const key of Object.keys(this.formGroup.controls)) {
            if (this.formGroup.controls[key].invalid) {
                const label = document.querySelector(`label[for="${key}"]`);
                label.scrollIntoView({ behavior: 'smooth' });
                // const invalidControl = document.querySelector('#' + key);
                // (<HTMLInputElement>invalidControl).focus();
                break;
            }
        }
    }

    onValidSubmit() {

    }

    ngOnDestroy(): void {
        this.subs.forEach(sub => {
            sub.unsubscribe();
        });
    }
}
