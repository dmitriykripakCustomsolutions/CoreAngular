import { CommonModule } from '@angular/common';
import { moduleMetadata } from '@storybook/angular';
import { Story } from '@storybook/angular/types-6-0';
import { HomeComponent } from '../home.component';

export default {
  title: 'Components/Home',
  component: HomeComponent,
  decorators: [
    moduleMetadata({
      declarations: [HomeComponent],
      imports: [CommonModule],
    }),
  ],
};

const Template: Story<HomeComponent> = () => ({
  component: HomeComponent  
});
export const Examples = Template.bind({});
