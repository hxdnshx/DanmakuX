using System.Text;

namespace Danmakux
{
    public class MotionHelper
    {
        private StringBuilder _publicBuilder = null;
        private StringBuilder _backupBuilder = new StringBuilder();
        private string _dstContainer = "";
        private string _dstBackup = "";
        private bool _isFirst = true;
        private bool _allBackupLayerRequired = false;

        public MotionHelper(StringBuilder builder, string dstContainer, string dstBackup)
        {
            _publicBuilder = builder;
            _dstContainer = dstContainer;
            _dstBackup = dstBackup;
        }

        public MotionHelper Apply(float duration,  TextProperty prop = null, string motion = "linear", bool isBackup = false)
        {
            //set b_3_1 {} 0.1s then set b_3_1 {x = 20%, y = 0%, rotateY = 0, alpha = 1} 1s, "ease-out" then set b_3_1{} 2s
            //then set b_3_1 {x = 20%, y = 150%, rotateY = 30, alpha = 0} 2s, "ease-in"
            var builder = isBackup ? _backupBuilder : _publicBuilder;
            bool isFirst = _isFirst;
            if (!_isFirst)
                builder.Append("then ");
            _isFirst = false;
            builder.Append($"set {(isBackup ? _dstBackup : _dstContainer)} {{");
            bool backupLayerRequired = false;
            for (;;)
            {
                if (prop == null) break;
                if (prop.x != null)
                {
                    builder.Append($"x={prop.x}%,");
                    prop.x = null;
                    backupLayerRequired = true;
                }

                if (prop.y != null)
                {
                    builder.Append($"y={prop.y}%,");
                    prop.y = null;
                    backupLayerRequired = true;
                }

                if (prop.rotateX != null && !backupLayerRequired)
                {
                    builder.Append($"rotateX={prop.rotateX},");
                    prop.rotateX = null;
                    backupLayerRequired = true;
                }

                if (prop.rotateY != null && !backupLayerRequired)
                {
                    builder.Append($"rotateY={prop.rotateY},");
                    prop.rotateY = null;
                    backupLayerRequired = true;
                }

                if (prop.rotateZ != null && !backupLayerRequired)
                {
                    builder.Append($"rotateZ={prop.rotateZ},");
                    prop.rotateX = null;
                    backupLayerRequired = true;
                }

                if (prop.scale != null && !backupLayerRequired)
                {
                    builder.Append($"scale={prop.scale},");
                    prop.scale = null;
                    backupLayerRequired = true;
                }

                if (prop.zIndex != null)
                {
                    builder.Append($"zIndex={prop.zIndex},");
                    prop.zIndex = null;
                }
                //if (prop.duration != null) _builder.Append($",duration={prop.duration}s");
                //if (prop.borderAlpha != null) _builder.Append($" borderAlpha={prop.borderAlpha}");
                //if (prop.borderWidth != null) _builder.Append($" borderWidth={prop.borderWidth}");
                //if (!string.IsNullOrEmpty(prop.borderColor)) _builder.Append($" borderColor={prop.borderColor}");
                //if (!string.IsNullOrEmpty(prop.fillColor)) _builder.Append($" fillColor={prop.fillColor}");
                //if (prop.fillAlpha != null) _builder.Append($" borderWidth={prop.fillAlpha}");

                if (prop.alpha != null)
                {
                    builder.Append($"alpha={prop.alpha},");
                    prop.alpha = null;
                }
                break;
            }

            builder.Append($"}} {duration}s");
            if (motion != "linear")
            {
                builder.Append($",\"{motion}\"");
            }
            builder.Append($"\n");

            if (!isBackup)
            {
                _isFirst = isFirst;
                if (prop!= null && 
                    (prop.rotateX != null || prop.rotateY != null || prop.rotateZ != null || prop.scale != null))
                    _allBackupLayerRequired = true;
                Apply(duration, prop, motion, true);
            }

            return this;
        }

        public void ProcessBackupLayer()
        {
            if (_allBackupLayerRequired)
            {
                _publicBuilder.Append(_backupBuilder.ToString());
            }
        }
    }
}