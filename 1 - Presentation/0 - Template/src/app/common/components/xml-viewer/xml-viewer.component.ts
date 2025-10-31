import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-xml-viewer',
  standalone: true,
  imports: [],
  template: `
  <pre style="white-space: pre-wrap;">
    <code class="language-xml">{{data.xmlString}}</code>
    </pre>
`,
  styleUrls: ['./xml-viewer.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class XmlViewerComponent {

  constructor(@Inject(MAT_DIALOG_DATA) public data: { xmlString: string }) { }

}
