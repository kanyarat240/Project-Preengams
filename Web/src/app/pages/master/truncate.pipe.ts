// truncate.pipe.ts
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncate'
})
export class TruncatePipe implements PipeTransform {
  transform(value: string, maxLength: number, ellipsis: string): string {
    if (value.length <= maxLength) {
      return value;
    }
    return value.substr(0, maxLength) + ellipsis;
  }
}
