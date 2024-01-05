// Importing necessary modules and services
import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/services/loader.service';

// Defining a type 'Tabs' with specific string values
type Tabs = 'Header' | 'Toolbar' | 'PageTitle' | 'Aside' | 'Content' | 'Footer';

// Angular Component decorator
@Component({
  selector: 'app-question', // Selector for the component
  templateUrl: './question.component.html', // Template URL
  styleUrls: ['./question.component.scss'], // Styles URL
})

// Class definition for the component implementing OnInit
export class questionComponent implements OnInit {

  constructor(private loaderService : LoaderService){}
    ngOnInit(): void {
      this.loaderService.startLoader()
      this.loaderService.stopLoader()
    }

  // Property to store the index of the currently expanded item
  currentExpandedIndex: number | undefined;

  // Hardcoded FAQ items
  items: any[] = [
    {
      question: 'ต้องเตรียมอะไรมาบ้าง ?',
      answer: 'ข้อเสนอแนะสำหรับการเตรียมของเพื่อเข้าพักอาศัย 1.เตรียมข้าวของเครื่องใช้ส่วนตัวของผู้พักอาศัย เช่น ชุดเครื่องนอน เครื่องอาบน้ำ เสื้อผ้า ยาประจำตัว อุปกรณ์อิเล็กทรอนิกส์ 2.เครื่องใช้ไฟฟ้าอื่นนอกเหนือจากที่ให้บริการ โปรดให้ผู้พักอาศัยนำมาด้วยตนเอง (เช่น พัดลม เครื่องฟอกอากาศ เครื่องเป่าผม ฯลฯ) 3.โปรดเตรียมหน้ากากอนามัยและแอลกอฮอล์ส่วนตัวไว้ใช้ เพื่อป้องกันการแพร่ระบาดของไวรัสโควิด19'
    },
    {
      question: 'นำสัตว์เลี้ยงมาพักได้หรือไม่ ?',
      answer: 'ทางหอพักไม่อนุญาตให้นำสัตว์เลี้ยงเข้าในที่พักอาศัย ทั้งนี้เพื่อให้เป็นประโยชน์ต่อการรักษาความสะอาดและบรรยากาศการพักอาศัยภายในอาคาร หลีกเลี่ยงอุบัติเหตุในสถานที่ซึ่งอาจเกิดขึ้นกับสัตว์เลี้ยงของคุณ ป้องกันปัญหาสุขภาพที่อาจเกิดขึ้นกับผู้พักอาศัยท่านอื่น',
    },
    {
        question: 'มีช่างซ่อมบำรุงเมื่อของในห้องชำรุดหรือไม่ ?',
        answer: 'ทางหอพัก มีบริการซ่อมบำรุงเครื่องใช้ที่ให้บริการภายในห้องพัก หากท่านพบว่ามีเครื่องใช้ดังกล่าวชำรุด สามารถแจ้งปัญหาให้เจ้าหน้าที่หอพักทราบได้ตามช่องทางต่อไปนี้  ติดต่อที่สำนักงานในเวลาทำการ 09:00 - 18:00 น. หรือโทรศัพท์ติดต่อเจ้าหน้าที่ได้ดังต่อไปนี้ คุณปู : 081-8078550 คุณกุ้ง : 086-3546084 หลังจากแจ้งแล้ว ทางเราจะส่งช่างซ่อมบำรุงไปยังห้องพักเพื่อสำรวจและแก้ไขปัญหาภายในเวลา 3-5 วัน',
    },
    {
        question: 'จะติดต่อเจ้าหน้าที่หอพักได้อย่างไร ?',
        answer: 'ท่านสามารถติดต่อเจ้าหน้าที่หอพักได้ตามช่องทางต่อไปนี้ ติดต่อที่สำนักงานในเวลาทำการ 09:00 - 18:00 น. คุณปู : 081-8078550 คุณกุ้ง : 086-3546084 ',
    },
    {
        question: 'มีเฟอร์นิเจอร์อะไรให้บ้าง ?',
        answer: 'ทาง The Green Place มีบริการเฟอร์นิเจอร์ในห้องพักดังต่อไปนี้ ตู้เสื้อผ้า 1 ใบ เตียงนอนห้าฟุต 1 เตียง โต๊ะเขียนหนังสือพร้อมเก้าอี้ 2 ชุด'
    },
    {
        question: 'ข้อห้ามหรือกฏของหอพักมีอะไรบ้าง ?',
        answer: 'กฏการพักอาศัยเป็นไปตามรายละเอียดในสัญญาเช่า เพื่อเป็นการรักษาบรรยากาศการพักอาศัยของทุกท่าน'
    }, 
    {
        question: 'มีบริการเช่าห้องพักรายวันหรือไม่ ?',
        answer: 'ทางหอพัก ไม่มีบริการให้ห้องเช่ารายวัน'
    }, 
    {
        question: 'มีพื้นที่ส่วนกลางหรือไม่ ?',
        answer: 'ขณะนี้ทางหอพัก ยังไม่มีบริการพื้นที่ส่วนกลาง'
    },
  ];

  // Method to toggle the expansion of items based on index
  toggleCompanyDetails(index: number) {
    if (this.currentExpandedIndex === index) {
      this.currentExpandedIndex = -1;
    } else {
      this.currentExpandedIndex = index;
    }
  }

  // Property to control the visibility of a section
  public opened = false;

  // Property to store the selected value (may be used in the template)
  selectedValue: any;

  // Method to open a section and set selectedValue to null
  public open(): void {
    this.selectedValue = null; // Setting selectedValue to null when opening for the first time
    this.opened = true;
  }
}
