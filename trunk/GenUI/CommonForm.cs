using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Business;
using DevExpress.XtraEditors;

namespace GenUI
{
    public partial class CommonForm : DevExpress.XtraEditors.XtraForm
    {
        Dictionary<string, object> FieldMap;
        EnumOperation enumOperation;
        Sys_Model curSys_Model;
        string Id;
        string connstr;
        public CommonForm(EnumOperation enumOperation, Sys_Model curSys_Model, string Id, Dictionary<string, object> FieldMap, string ConnectionStr)
        {
            this.enumOperation = enumOperation;
            this.curSys_Model = curSys_Model;
            this.Id = Id;
            connstr = ConnectionStr;
            this.FieldMap = FieldMap;
            InitializeComponent();
            InitControls();
        }
        private void CommonForm_Load(object sender, EventArgs e)
        {
        }
        private Control GetControl(string typ)
        {
            switch (typ)
            {
                case "Label":
                    return new Label();
                case "DevExpress.XtraEditors.TextEdit":
                    return new TextEdit();
                case "DevExpress.XtraEditors.ButtonEdit":
                    return new ButtonEdit();
                case "DevExpress.XtraEditors.DateEdit":
                    return new DateEdit();
                case "DevExpress.XtraEditors.CheckEdit":
                    return new CheckEdit();
                case "DevExpress.XtraEditors.ComboBoxEdit":
                    return new ComboBoxEdit();
                case "DevExpress.XtraEditors.MemoEdit":
                    return new MemoEdit();
                case "DevExpress.XtraEditors.SpinEdit":
                    return new SpinEdit();
                case "LabelEdit":
                    return new Label();
            }
            return new Label();
        }
        private void SaveToFieldMap(string fieldname, object valueobj)
        {
            if (FieldMap.ContainsKey(fieldname))
            {
                FieldMap[fieldname] = valueobj;
            }
            else
            {
                FieldMap.Add(fieldname, valueobj);
            }
        }
        public void InitControls()
        {
            try
            {
                DiyForm form = curSys_Model.UIXml.XmlDeserialize<DiyForm>();
                this.Width = form.Width + 9;
                this.Height = form.Height + 40;
                this.Text = curSys_Model.TableTitle;
                //BindObjectData();

                foreach (Ctrl c in form.CtrlList)
                {
                    Control ctrl = GetControl(c.tp);
                    ctrl.Location = c.p;
                    ctrl.Size = c.s;
                    ctrl.Name = c.FieldName;
                    this.panelControl1.Controls.Add(ctrl);
                    Sys_ModelAttribute sys_ModelAttribute = null;
                    if (c.FieldName != null)
                        sys_ModelAttribute =Environment.Logic.GetAll<Sys_ModelAttribute>(p => p.AttrName == c.FieldName)[0];
                    if (c.FieldName != null && FieldMap.ContainsKey(c.FieldName))
                    {
                        ctrl.Tag = FieldMap[c.FieldName];
                        if (c.FieldName.EndsWith("Id"))
                        {
                            string text = c.FieldName.Remove(c.FieldName.Length - 2);
                            if (FieldMap.ContainsKey(text))
                            {
                                ctrl.Tag = FieldMap[text];
                            }
                        }
                    }
                    if (sys_ModelAttribute != null && enumOperation == EnumOperation.Add)
                    {   // 初始化。
                        if (ctrl is ButtonEdit)
                        {
                            ButtonEdit be = ctrl as ButtonEdit;
                            SaveToFieldMap(be.Name.ToString().Remove(be.Name.ToString().Length - 2), be.Text);
                            SaveToFieldMap(be.Name.ToString(), be.Tag.ToString());
                        }
                    }
                    if (ctrl is DateEdit)
                    {
                        DateEdit rtb = ctrl as DateEdit;
                        if (ctrl.Tag != null && !(ctrl.Tag is DBNull))
                        {
                            rtb.DateTime = Convert.ToDateTime(ctrl.Tag.ToString());
                        }
                    }
                    if (ctrl is TextEdit)
                    {
                        TextEdit rtb = ctrl as TextEdit;
                        if (ctrl.Tag != null)
                        {
                            rtb.Text = ctrl.Tag.ToString();
                        }
                    }
                    if (ctrl is SpinEdit)
                    {
                        SpinEdit be = ctrl as SpinEdit;
                        if (sys_ModelAttribute.AttrControl.ParseTo<int>() == (int)EnumControl.NumBoxSmall)

                            if (ctrl.Tag != null && !(ctrl.Tag is DBNull))
                            {
                                be.Value = Convert.ToDecimal(ctrl.Tag);
                            }
                    }
                    if (ctrl is Label)
                    {
                        ctrl.Text = c.txt;
                    }
                    if (ctrl is ComboBoxEdit)
                    {
                        ComboBoxEdit cbo = ctrl as ComboBoxEdit;
                        //cbo.LookAndFeel = Telerik.WinControls.RadDropDownStyle.DropDownList;
                        //EnumItemFacade enumItemFacade = new EnumItemFacade();
                        //List<EnumItem> list = enumItemFacade.GetList(p => p.EnumKey == nodeAttribute.EnumKey);
                        //list.ForEach(p =>
                        //    {
                        //        cbo.Items.Add(new RadListDataItem(p.EnumText, p.EnumValue));
                        //    });
                        //if (ctrl.Tag != null)
                        //{
                        //    if (list.Find(p => p.EnumText == ctrl.Tag.ToString()) == null)
                        //    {
                        //        //老数据的枚举已存储 而枚举没找到 则显示成默认值。
                        //    }
                        //    else
                        //    {
                        //        EnumItem ei = list.Find(p => p.EnumText == ctrl.Tag.ToString());
                        //        cbo.SelectedItem = cbo.Items.First(p => p.Value.ToString() == ei.EnumValue);
                        //    }
                        //}
                        //if (cbo.SelectedItem == null)
                        //    cbo.SelectedItem = cbo.Items[0];
                        //cbo.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(cbo_SelectedIndexChanged);
                    }
                    if (ctrl is CheckEdit)
                    {
                        CheckEdit cb = ctrl as CheckEdit;
                        if (ctrl.Tag != null)
                        {
                            cb.Checked = (bool)ctrl.Tag;
                        }
                        cb.CheckedChanged += new EventHandler(cb_CheckedChanged);
                    }
                    if (ctrl is ButtonEdit)
                    {
                        ButtonEdit be = ctrl as ButtonEdit;


                        if (ctrl.Tag != null)
                        {
                            be.Text = ctrl.Tag.ToString();
                            if (FieldMap.ContainsKey(c.FieldName))
                                ctrl.Tag = FieldMap[c.FieldName];
                        }

                        be.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(be_ButtonClick);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void be_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}