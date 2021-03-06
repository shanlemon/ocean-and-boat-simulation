from manim import *
import math

class SquareToCircle(Scene):
    def construct(self):
        circle = Circle()  # create a circle
        circle.set_fill(PINK, opacity=0.5)  # set color and transparency

        square = Square()  # create a square
        square.flip(RIGHT)  # flip horizontally
        square.rotate(-3 * TAU / 8)  # rotate a certain amount

        self.play(Create(square))  # animate the creation of the square
        self.play(Transform(square, circle))  # interpolate the square into the circle
        self.play(FadeOut(square))  # fade out animation


class WaveFunction(Scene):
  def construct(self):
    circle = Circle(radius=1, color=BLUE)
    dot = Dot()
    dot2 = dot.copy().shift(RIGHT)
    self.add(dot)

    self.origin_point = np.array([0,0,0])

    def get_line_to_circle():
            return Line(self.origin_point, dot.get_center(), color=BLUE)

    origin_to_circle_line = always_redraw(get_line_to_circle)

    self.play(GrowFromCenter(circle))
    self.play(Transform(dot, dot2))
    self.add(origin_to_circle_line)
    self.play(MoveAlongPath(dot, circle), run_time=5, rate_func=linear)
    self.play(MoveAlongPath(dot, circle), run_time=5, rate_func=linear)
    self.wait()


class TextTest(Scene):
  def construct(self):
    text = Text("""
            In general, using the interactive shell
            is very helpful when developing new scenes
        """)
    self.play(Write(text))


class SineCurveUnitCircle(Scene):
  # contributed by heejin_park, https://infograph.tistory.com/230
  def construct(self):
    self.wait(1.5)
    self.show_axis()
    self.show_circle()
    self.move_dot_and_draw_curve()
    self.hide_lines_and_move_circle_to_center()

    self.additional_dots = []
    self.num_circles = 0
    circles = []
    for i in range(1, 5):
      circles += self.duplicate_circles(i, i/10)
      circles += self.duplicate_circles(-i, -i/10)
    # self.play(*(self.duplicate_circles(1, 0.1) + self.duplicate_circles(-1, -0.1) + self.duplicate_circles(-2, -0.2) + self.duplicate_circles(2, 0.2) + self.duplicate_circles(3, 0.3) + 
    #             self.duplicate_circles(-3, -0.3)))
    self.play(*(circles))

    self.show_line_wave()


    self.wait(10)

  def show_axis(self):
    x_start = np.array([-6,0,0])
    x_end = np.array([6,0,0])

    y_start = np.array([-4,-2,0])
    y_end = np.array([-4,2,0])

    self.x_axis = Line(x_start, x_end)
    self.y_axis = Line(y_start, y_end)

    self.x_axis_text = MathTex("x").next_to(self.x_axis, LEFT)

    self.y_axis_text = MathTex("y=sin(x)")
    # text = MathTex("y=sin(x)").next_to(y_axis,UP).shift(RIGHT)

    self.play(Write(self.y_axis_text))
    self.wait(3)
    self.play(ApplyMethod(self.y_axis_text.shift, LEFT * 3 + UP * 2.5))

    self.play(Create(self.x_axis), Create(self.y_axis), Create(self.x_axis_text), *self.get_x_labels())
    # self.add(x_axis_text)
    # self.add_x_labels()

    self.origin_point = np.array([-4,0,0])
    self.curve_start = np.array([-3,0,0])

  def get_x_labels(self):

    self.x_labels = [
      MathTex("\pi"), MathTex("2 \pi"),
      MathTex("3 \pi"), MathTex("4 \pi"),
    ]
    labels = []

    for i in range(len(self.x_labels)):
      self.x_labels[i].next_to(np.array([-1 + 2*i, 0, 0]), DOWN)
      # self.add(self.x_labels[i])
      labels.append(Create(self.x_labels[i]))
    return labels

  def show_circle(self):
    circle = Circle(radius=1)
    circle.move_to(self.origin_point)
    self.play(Create(circle))
    self.circle = circle

  def move_dot_and_draw_curve(self):
    orbit = self.circle
    origin_point = self.origin_point

    self.dot = Dot(radius=0.08, color=YELLOW)
    dot = self.dot
    dot.move_to(orbit.point_from_proportion(0))
    self.t_offset = 0
    self.rate = 0.15
    rate = self.rate

    self.origin_dot = Dot(radius=0.08, color=YELLOW).move_to(origin_point)

    def go_around_circle(mob, dt):
      self.t_offset += (dt * rate)
      mob.move_to(orbit.point_from_proportion(self.t_offset % 1))

    def get_line_to_circle():
      return Line(self.origin_dot.get_center(), dot.get_center(), color=BLUE)

    def get_line_to_curve():
      x = self.curve_start[0] + self.t_offset * 4
      y = dot.get_center()[1]
      return Line(dot.get_center(), np.array([x,y,0]), color=YELLOW_A, stroke_width=2 )


    self.curve = VGroup()
    self.curve.add(Line(self.curve_start,self.curve_start))
    def get_curve():
      last_line = self.curve[-1]
      x = self.curve_start[0] + self.t_offset * 4
      y = dot.get_center()[1]
      new_line = Line(last_line.get_end(),np.array([x,y,0]), color=YELLOW_D)
      self.curve.add(new_line)

      return self.curve

    dot.add_updater(go_around_circle)

    self.origin_to_circle_line = always_redraw(get_line_to_circle)
    self.dot_to_curve_line = always_redraw(get_line_to_curve)
    self.sine_curve_line = always_redraw(get_curve)

    self.add(self.origin_dot)
    self.add(orbit, dot, self.origin_to_circle_line, self.dot_to_curve_line, self.sine_curve_line)
    self.wait(13)

    dot.remove_updater(go_around_circle)
  def hide_lines_and_move_circle_to_center(self):
    def go_around_circle(mob, dt):
      self.t_offset += (dt * 0.25)
      mob.move_to(self.circle.point_from_proportion(self.t_offset % 1))
    
    labels = []
    for i in range(len(self.x_labels)):
      self.x_labels[i].next_to(np.array([-1 + 2*i, 0, 0]), DOWN)
      # self.add(self.x_labels[i])
      labels.append(FadeOut(self.x_labels[i]))

    self.play(FadeOut(self.x_axis), FadeOut(self.y_axis), FadeOut(self.x_axis_text), FadeOut(self.y_axis_text),
              FadeOut(self.dot_to_curve_line), FadeOut(self.sine_curve_line), *labels)

    self.dot.add_updater(go_around_circle)
    self.origin_point = np.array([0,0,0])
    self.play(ApplyMethod(self.circle.move_to, self.origin_point), 
              ApplyMethod(self.origin_dot.move_to, self.origin_point))
    self.wait(2)

  def duplicate_circles(self, num, shift):

    circle2 = self.circle.copy().shift(RIGHT * num)
    dot2 = Dot(radius=0.08, color=YELLOW).move_to(np.array([num,0,0]))
    origin_dot_2 = self.origin_dot.copy().shift(RIGHT * num)
    
    def get_line_to_circle():
      return Line(origin_dot_2.get_center(), dot2.get_center(), color=BLUE)

    self.rate = 0.05
    def go_around_circle(mob, dt):
      self.t_offset += (dt * self.rate)
      mob.move_to(circle2.point_from_proportion((self.t_offset + shift) % 1))
    
    dot2.add_updater(go_around_circle)

    line2 = always_redraw(get_line_to_circle)

    self.num_circles += 1
    self.additional_dots.append(dot2) 

    return [Create(circle2), Create(dot2), Create(origin_dot_2), Create(line2)]

  def show_line_wave(self):
    # def get_lines_betweem_dots():
    #   lines = []
    #   i = 1
    #   while i < len(self.additional_dots):
    #     lines += Line(self.additional_dots[i-1].get_center(), self.additional_dots[i].get_center(), color=BLUE)
    #     i += 1
    #   return lines[0]
    line = Line(self.additional_dots[0].get_center(), self.additional_dots[1].get_center(), color=BLUE)
    Create(line)

    
    
    