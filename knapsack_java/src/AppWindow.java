import java.awt.*;
import java.awt.event.*;
import javax.swing.*;


public class AppWindow implements ActionListener{
    JFrame frame;
    JButton countButton;
    JTextField textF;
    JLabel resultJL;
    Item[] items = KnapsackMain.randomizeItems(8, 12345, 1, 29, 1, 29);
    Item[] solution = KnapsackMain.solve(15, items);


    public void zbudujGUI() {
        frame = new JFrame();
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(400, 200);
        countButton = new JButton();
        countButton.setText("Oblicz!");
        countButton.addActionListener(this);
        JPanel jp = new JPanel();
        jp.setLayout(new GridLayout(1,2));
        jp.add(countButton);
        JPanel jp2 = new JPanel();
        jp2.setLayout(new GridLayout(3,1));
        textF = new JTextField("Wybierz przedział wartości przedmiotów oraz wag!");
        resultJL = new JLabel();
        resultJL.setSize(400,20);
        jp2.add(textF);
        jp2.add(resultJL);
        jp2.add(jp);
        frame.add(jp2);
        frame.setVisible(true);
    }


    public void actionPerformed(ActionEvent akcja) {
        if (akcja.getSource() == countButton){
            for (var item: solution) {
                this.resultJL.setText("<html><center>"+ resultJL.getText() + item.toString() + "<br>");
            }
        }
        else
            System.out.println("Ups");

    }

}