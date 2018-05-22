/*

This file is part of the iText (R) project.
Copyright (c) 1998-2017 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using System;
using iText.IO.Log;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Navigation;

namespace iText.Kernel.Pdf.Annot {
    public class PdfLinkAnnotation : PdfAnnotation {
        private static readonly ILogger logger = LoggerFactory.GetLogger(typeof(iText.Kernel.Pdf.Annot.PdfLinkAnnotation
            ));

        /// <summary>Highlight modes.</summary>
        public static readonly PdfName None = PdfName.N;

        public static readonly PdfName Invert = PdfName.I;

        public static readonly PdfName Outline = PdfName.O;

        public static readonly PdfName Push = PdfName.P;

        /// <param name="pdfObject">object representing this annotation</param>
        [System.ObsoleteAttribute(@"Use PdfAnnotation.MakeAnnotation(iText.Kernel.Pdf.PdfObject) instead. Will be made protected in 7.1"
            )]
        public PdfLinkAnnotation(PdfDictionary pdfObject)
            : base(pdfObject) {
        }

        public PdfLinkAnnotation(Rectangle rect)
            : base(rect) {
        }

        public override PdfName GetSubtype() {
            return PdfName.Link;
        }

        public virtual PdfObject GetDestinationObject() {
            return GetPdfObject().Get(PdfName.Dest);
        }

        public virtual iText.Kernel.Pdf.Annot.PdfLinkAnnotation SetDestination(PdfObject destination) {
            if (GetPdfObject().ContainsKey(PdfName.A)) {
                GetPdfObject().Remove(PdfName.A);
                logger.Warn(iText.IO.LogMessageConstant.DESTINATION_NOT_PERMITTED_WHEN_ACTION_IS_SET);
            }
            if (destination.IsArray() && ((PdfArray)destination).Get(0).IsNumber()) {
                LoggerFactory.GetLogger(typeof(iText.Kernel.Pdf.Annot.PdfLinkAnnotation)).Warn(iText.IO.LogMessageConstant
                    .INVALID_DESTINATION_TYPE);
            }
            return (iText.Kernel.Pdf.Annot.PdfLinkAnnotation)Put(PdfName.Dest, destination);
        }

        public virtual iText.Kernel.Pdf.Annot.PdfLinkAnnotation SetDestination(PdfDestination destination) {
            return SetDestination(destination.GetPdfObject());
        }

        public virtual iText.Kernel.Pdf.Annot.PdfLinkAnnotation RemoveDestination() {
            GetPdfObject().Remove(PdfName.Dest);
            return this;
        }

        /// <summary>
        /// An
        /// <see cref="iText.Kernel.Pdf.Action.PdfAction"/>
        /// to perform, such as launching an application, playing a sound,
        /// changing an annotation’s appearance state etc, when the annotation is activated.
        /// </summary>
        /// <returns>
        /// 
        /// <see cref="iText.Kernel.Pdf.PdfDictionary"/>
        /// which defines the characteristics and behaviour of an action.
        /// </returns>
        public override PdfDictionary GetAction() {
            return GetPdfObject().GetAsDictionary(PdfName.A);
        }

        /// <summary>
        /// Sets a
        /// <see cref="iText.Kernel.Pdf.PdfDictionary"/>
        /// representing action to this annotation which will be performed
        /// when the annotation is activated.
        /// </summary>
        /// <param name="action">
        /// 
        /// <see cref="iText.Kernel.Pdf.PdfDictionary"/>
        /// that represents action to set to this annotation.
        /// </param>
        /// <returns>
        /// this
        /// <see cref="PdfLinkAnnotation"/>
        /// instance.
        /// </returns>
        public virtual iText.Kernel.Pdf.Annot.PdfLinkAnnotation SetAction(PdfDictionary action) {
            return (iText.Kernel.Pdf.Annot.PdfLinkAnnotation)Put(PdfName.A, action);
        }

        /// <summary>
        /// Sets a
        /// <see cref="iText.Kernel.Pdf.Action.PdfAction"/>
        /// to this annotation which will be performed when the annotation is activated.
        /// </summary>
        /// <param name="action">
        /// 
        /// <see cref="iText.Kernel.Pdf.Action.PdfAction"/>
        /// to set to this annotation.
        /// </param>
        /// <returns>
        /// this
        /// <see cref="PdfLinkAnnotation"/>
        /// instance.
        /// </returns>
        public override PdfAnnotation SetAction(PdfAction action) {
            if (GetDestinationObject() != null) {
                RemoveDestination();
                logger.Warn(iText.IO.LogMessageConstant.ACTION_WAS_SET_TO_LINK_ANNOTATION_WITH_DESTINATION);
            }
            return (iText.Kernel.Pdf.Annot.PdfLinkAnnotation)Put(PdfName.A, action.GetPdfObject());
        }

        public virtual iText.Kernel.Pdf.Annot.PdfLinkAnnotation RemoveAction() {
            GetPdfObject().Remove(PdfName.A);
            return this;
        }

        public virtual PdfName GetHighlightMode() {
            return GetPdfObject().GetAsName(PdfName.H);
        }

        public virtual iText.Kernel.Pdf.Annot.PdfLinkAnnotation SetHighlightMode(PdfName hlMode) {
            return (iText.Kernel.Pdf.Annot.PdfLinkAnnotation)Put(PdfName.H, hlMode);
        }

        public virtual PdfDictionary GetUriActionObject() {
            return GetPdfObject().GetAsDictionary(PdfName.PA);
        }

        public virtual iText.Kernel.Pdf.Annot.PdfLinkAnnotation SetUriAction(PdfDictionary action) {
            return (iText.Kernel.Pdf.Annot.PdfLinkAnnotation)Put(PdfName.PA, action);
        }

        public virtual iText.Kernel.Pdf.Annot.PdfLinkAnnotation SetUriAction(PdfAction action) {
            return (iText.Kernel.Pdf.Annot.PdfLinkAnnotation)Put(PdfName.PA, action.GetPdfObject());
        }

        /// <summary>An array of 8 × n numbers specifying the coordinates of n quadrilaterals in default user space.</summary>
        /// <remarks>
        /// An array of 8 × n numbers specifying the coordinates of n quadrilaterals in default user space.
        /// Quadrilaterals are used to define regions inside annotation rectangle
        /// in which the link annotation should be activated.
        /// </remarks>
        /// <returns>
        /// an
        /// <see cref="iText.Kernel.Pdf.PdfArray"/>
        /// of 8 × n numbers specifying the coordinates of n quadrilaterals.
        /// </returns>
        public override PdfArray GetQuadPoints() {
            return GetPdfObject().GetAsArray(PdfName.QuadPoints);
        }

        /// <summary>
        /// Sets n quadrilaterals in default user space by passing an
        /// <see cref="iText.Kernel.Pdf.PdfArray"/>
        /// of 8 × n numbers.
        /// Quadrilaterals are used to define regions inside annotation rectangle
        /// in which the link annotation should be activated.
        /// </summary>
        /// <param name="quadPoints">
        /// an
        /// <see cref="iText.Kernel.Pdf.PdfArray"/>
        /// of 8 × n numbers specifying the coordinates of n quadrilaterals.
        /// </param>
        /// <returns>
        /// this
        /// <see cref="PdfLinkAnnotation"/>
        /// instance.
        /// </returns>
        public override PdfAnnotation SetQuadPoints(PdfArray quadPoints) {
            return (iText.Kernel.Pdf.Annot.PdfLinkAnnotation)Put(PdfName.QuadPoints, quadPoints);
        }

        /// <summary>
        /// BS entry specifies a border style dictionary that has more settings than the array specified for the Border
        /// entry (see
        /// <see cref="PdfAnnotation.GetBorder()"/>
        /// ). If an annotation dictionary includes the BS entry, then the Border
        /// entry is ignored. If annotation includes AP (see
        /// <see cref="PdfAnnotation.GetAppearanceDictionary()"/>
        /// ) it takes
        /// precedence over the BS entry. For more info on BS entry see ISO-320001, Table 166.
        /// </summary>
        /// <returns>
        /// 
        /// <see cref="iText.Kernel.Pdf.PdfDictionary"/>
        /// which is a border style dictionary or null if it is not specified.
        /// </returns>
        public override PdfDictionary GetBorderStyle() {
            return GetPdfObject().GetAsDictionary(PdfName.BS);
        }

        /// <summary>
        /// Sets border style dictionary that has more settings than the array specified for the Border entry (
        /// <see cref="PdfAnnotation.GetBorder()"/>
        /// ).
        /// See ISO-320001, Table 166 and
        /// <see cref="GetBorderStyle()"/>
        /// for more info.
        /// </summary>
        /// <param name="borderStyle">
        /// a border style dictionary specifying the line width and dash pattern that shall be used
        /// in drawing the annotation’s border.
        /// </param>
        /// <returns>
        /// this
        /// <see cref="PdfLinkAnnotation"/>
        /// instance.
        /// </returns>
        public override PdfAnnotation SetBorderStyle(PdfDictionary borderStyle) {
            return (iText.Kernel.Pdf.Annot.PdfLinkAnnotation)Put(PdfName.BS, borderStyle);
        }

        /// <summary>Setter for the annotation's preset border style.</summary>
        /// <remarks>
        /// Setter for the annotation's preset border style. Possible values are
        /// <ul>
        /// <li>
        /// <see cref="PdfAnnotation.STYLE_SOLID"/>
        /// - A solid rectangle surrounding the annotation.</li>
        /// <li>
        /// <see cref="PdfAnnotation.STYLE_DASHED"/>
        /// - A dashed rectangle surrounding the annotation.</li>
        /// <li>
        /// <see cref="PdfAnnotation.STYLE_BEVELED"/>
        /// - A simulated embossed rectangle that appears to be raised above the surface of the page.</li>
        /// <li>
        /// <see cref="PdfAnnotation.STYLE_INSET"/>
        /// - A simulated engraved rectangle that appears to be recessed below the surface of the page.</li>
        /// <li>
        /// <see cref="PdfAnnotation.STYLE_UNDERLINE"/>
        /// - A single line along the bottom of the annotation rectangle.</li>
        /// </ul>
        /// See also ISO-320001, Table 166.
        /// </remarks>
        /// <param name="style">The new value for the annotation's border style.</param>
        /// <returns>
        /// this
        /// <see cref="PdfLinkAnnotation"/>
        /// instance.
        /// </returns>
        /// <seealso cref="GetBorderStyle()"/>
        public override PdfAnnotation SetBorderStyle(PdfName style) {
            return ((iText.Kernel.Pdf.Annot.PdfLinkAnnotation)SetBorderStyle(BorderStyleUtil.SetStyle(GetBorderStyle()
                , style)));
        }

        /// <summary>Setter for the annotation's preset dashed border style.</summary>
        /// <remarks>
        /// Setter for the annotation's preset dashed border style. This property has affect only if
        /// <see cref="PdfAnnotation.STYLE_DASHED"/>
        /// style was used for the annotation border style (see
        /// <see cref="SetBorderStyle(iText.Kernel.Pdf.PdfName)"/>
        /// .
        /// See ISO-320001 8.4.3.6, “Line Dash Pattern” for the format in which dash pattern shall be specified.
        /// </remarks>
        /// <param name="dashPattern">
        /// a dash array defining a pattern of dashes and gaps that
        /// shall be used in drawing a dashed border.
        /// </param>
        /// <returns>
        /// this
        /// <see cref="PdfLinkAnnotation"/>
        /// instance.
        /// </returns>
        public override PdfAnnotation SetDashPattern(PdfArray dashPattern) {
            return ((iText.Kernel.Pdf.Annot.PdfLinkAnnotation)SetBorderStyle(BorderStyleUtil.SetDashPattern(GetBorderStyle
                (), dashPattern)));
        }
    }
}
